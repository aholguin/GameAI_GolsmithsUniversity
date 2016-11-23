using UnityEngine;
using System.Collections.Generic;

public interface Graph
{

    bool addNode(int a);          // true if node added
    bool addEdge(int a, int b);   // true if edge added

    List<int> nodes();

    List<int> neighbours(int a);

	int cost (int a, int b);

}