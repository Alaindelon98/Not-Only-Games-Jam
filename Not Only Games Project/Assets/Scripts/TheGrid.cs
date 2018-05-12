using System.Collections.Generic;
using UnityEngine;
using System;

public class TheGrid : MonoBehaviour
{

    public int m_size_x;
    public int m_size_y;
    public int m_nodeSize;


    private int currentMinF;
    private int currentMinH;


    public CNode[,] grid;
    public List<CNode> portals = new List<CNode>();

    private void Awake()
    {
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        grid = new CNode[m_size_x, m_size_y];

        for (int i = 0; i < m_size_x; i++)
        {
            for (int j = 0; j < m_size_y; j++)
            {

                Vector3 nodePosition = new Vector3(i * m_nodeSize + 0.5f * m_nodeSize, j * m_nodeSize + 0.5f * m_nodeSize, 0); // multiplicar es bien dividir es mal
                Vector3 worldPosition = nodePosition + transform.position;
                Collider[] colliders = Physics.OverlapSphere(worldPosition, m_nodeSize * 0.5f);

                bool isTransitable = true;
                for (int k = 0; k < colliders.Length; k++)
                {
                    if (colliders[k].tag == "Wall")
                        isTransitable = false;



                }


                grid[i, j] = new CNode(i, j, m_nodeSize, worldPosition, isTransitable);
                //grid[i, j].Grita();

            }
        }
    }

    public CNode GetNodeContainingPosition(Vector3 worldPosition)
    {
        Vector3 localPosition = worldPosition - transform.position;

        int x = Mathf.FloorToInt(localPosition.x / m_nodeSize);
        int y = (int)localPosition.y / m_nodeSize;


        if (x < m_size_x && x >= 0 && x < m_size_y && y >= 0)
        {
            return grid[x, y];
        }
        return null;
    }
    public CNode GetNode(int x, int y)
    {
        if (x < 0 || y < 0 || x >= m_size_x || y >= m_size_y)
        {
            Debug.LogWarning("Se ha pedido un nodo FUERA de la grid con posicion : " + x + ", " + y);

            return null;
        }


        return grid[x, y];
    }




    public List<CNode> GetNeightbours(CNode nodo, bool extended)
    {

        List<CNode> listaDeNodosADevolver = new List<CNode>();
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (!extended)
                {
                    if (Mathf.Abs(i) == Mathf.Abs(j))
                        continue;
                }

                if (i == 0 && j == 0)
                    continue;

                CNode vecino = GetNode(nodo.gridPositionX + i, nodo.gridPositionY + j);
                if (vecino != null)
                {
                    if (!vecino.isTransitable)
                        Debug.LogWarning("Se ha pedido un nodo OCUPADO de la grid con posicion   " + nodo.position);
                    else
                    {
                        listaDeNodosADevolver.Add(vecino);
                    }
                }

            }
        }

        return listaDeNodosADevolver;
    }
    public CNode GetNeightbourRight(CNode origin)
    {
        CNode nodeToReturn = GetNode(origin.gridPositionX + 1, origin.gridPositionY);
        return nodeToReturn;
    }
    public CNode GetNeightbourLeft(CNode origin)
    {
        CNode nodeToReturn = GetNode(origin.gridPositionX - 1, origin.gridPositionY);
        return nodeToReturn;
    }
    public CNode GetNeightbourUp(CNode origin)
    {
        CNode nodeToReturn = GetNode(origin.gridPositionX, origin.gridPositionY + 1);

        return nodeToReturn;
    }
    public CNode GetNeightbourDown(CNode origin)
    {
        CNode nodeToReturn = GetNode(origin.gridPositionX, origin.gridPositionY - 1);
        return nodeToReturn;
    }

    public CNode GetRandomNode()
    {
        List<CNode> TransitableNodes = new List<CNode>();
        System.Random alea = new System.Random();

        foreach (CNode n in grid)
        {
            if (n.isTransitable)
                TransitableNodes.Add(n);
        }

        if (TransitableNodes != null)
            return TransitableNodes[alea.Next(0, TransitableNodes.Count)];
        else
            return null;


    }

    private void SetNodeFather(CNode children, CNode father)
    {
        children.fatherNode = father;
    }

    public int GetH(CNode current, CNode target)
    {
        return Math.Abs(target.gridPositionY - current.gridPositionY) +
            Math.Abs(target.gridPositionX - current.gridPositionX);
    }

    private int GetG(CNode origin, CNode current)
    {
        return Math.Abs(current.gridPositionY - origin.gridPositionY) +
            Math.Abs(current.gridPositionX - origin.gridPositionX);
    }

    private int GetF(int h, int g)
    {
        return h + g;
    }

    private List<CNode> GetFamilyOfANode(CNode nodeYounger, CNode nodeOldest)
    {

        List<CNode> familyOfNodes = new List<CNode>();
        CNode familiarNode = nodeYounger;

        if (familiarNode.fatherNode != null && nodeOldest != null)
        {
            while (familiarNode != nodeOldest)
            {
                familyOfNodes.Add(familiarNode);
                familiarNode = familiarNode.fatherNode;
            }
        }
        familyOfNodes.Reverse();

        return familyOfNodes;
    }


    private CNode GetCheapestOpenNode(List<CNode> nodes)
    {
        foreach (CNode n in nodes)
        {
            if (n.distanceF < currentMinF)
            {
                currentMinF = n.distanceF;
                currentMinH = n.distanceH;
                return n;
            }
            else if (n.distanceF == currentMinF && n.distanceH <= currentMinH)
            {
                currentMinF = n.distanceF;
                currentMinH = n.distanceH;
                return n;
            }
        }


        return null;
    }

    public List<CNode> AEstrella(CNode origin, CNode target)
    {

        List<CNode> openNodes = new List<CNode>();
        List<CNode> closedNodes = new List<CNode>();
        List<CNode> path = new List<CNode>();
        CNode currentNode;

        currentMinH = m_size_x * m_size_y;
        currentMinF = m_size_x * m_size_y;

        origin.distanceH = GetH(origin, target);
        origin.distanceG = GetG(origin, origin);
        origin.distanceF = GetF(origin.distanceH, origin.distanceG);
        openNodes.Add(origin);


        while (openNodes.Count > 0)
        {
            currentNode = GetCheapestOpenNode(openNodes);
            currentMinH = m_size_x * m_size_y;
            currentMinF = m_size_x * m_size_y;


            closedNodes.Add(currentNode);
            openNodes.Remove(currentNode);

            if (currentNode == target)
            {
                path = GetFamilyOfANode(currentNode, origin); //returns the full path in the correct order
                return path;
            }
            else
            {
                foreach (CNode neighbour in GetNeightbours(currentNode, false))
                {
                    if (!closedNodes.Contains(neighbour)) //check if it's not a closed node
                    {
                        if (!openNodes.Contains(neighbour) || GetG(origin, neighbour) < neighbour.distanceG)
                        {
                            neighbour.distanceH = GetH(neighbour, target);
                            neighbour.distanceG = GetG(origin, neighbour);
                            neighbour.distanceF = GetF(neighbour.distanceH, neighbour.distanceG);

                            SetNodeFather(neighbour, currentNode);

                            if (!openNodes.Contains(neighbour))
                            {
                                openNodes.Add(neighbour);
                            }
                        }
                    }
                }
            }
        }






        return null;
    }


    private void OnDrawGizmosSelected()
    {

        if (grid != null)
        {

            Gizmos.color = Color.green;

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    //Vector3 position = new Vector3(i*nodeSize + 0.5f*nodeSize, j*nodeSize + 0.5f*nodeSize, 0); // multiplicar es bien dividir es mal
                    Vector3 scale = new Vector3(m_nodeSize, m_nodeSize, m_nodeSize);
                    if (!grid[i, j].isTransitable)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawCube(grid[i, j].position, scale);
                        Gizmos.color = Color.green;


                    }
                    else
                    {
                        Gizmos.DrawWireCube(grid[i, j].position, scale);

                    }


                }
            }
        }
    }


}