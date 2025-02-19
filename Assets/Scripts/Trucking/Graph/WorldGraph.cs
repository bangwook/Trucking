using System;
using System.Collections.Generic;
using System.Linq;
using Trucking.Model;

namespace Trucking.Graph
{
    public class WorldGraph : Graph
    {
        public WorldGraphPath GetShortestPath(City start, City end)
        {
            
            // We don't accept null arguments
			if ( start == null || end == null )
			{
				throw new ArgumentNullException ();
			}
			
			// The final path
			WorldGraphPath graphPath = new WorldGraphPath ();
	
			// If the start and end are same node, we can return the start node
			if ( start == end )
			{
				graphPath.nodes.Add ( start );
				return graphPath;
			}
			
			// The list of unvisited nodes
			List<City> unvisited = new List<City> ();
			
			// Previous nodes in optimal path from source
			Dictionary<City, City> previous = new Dictionary<City, City> ();
			
			// The calculated distances, set all to Infinity at start, except the start Node
			Dictionary<City, float> distances = new Dictionary<City, float> ();
			
			for ( int i = 0; i < m_Nodes.Count; i++ )
			{
				City node = m_Nodes [ i ] as City;
				unvisited.Add ( node );
				
				// Setting the node distance to Infinity
				distances.Add ( node, float.MaxValue );
			}
			
			// Set the starting Node distance to zero
			distances [ start ] = 0f;
			while ( unvisited.Count != 0 )
			{
				
				// Ordering the unvisited list by distance, smallest distance at start and largest at end
				unvisited = unvisited.OrderBy ( node => distances [ node ] ).ToList ();
				
				// Getting the Node with smallest distance
				City current = unvisited [ 0 ];
				
				// Remove the current node from unvisisted list
				unvisited.Remove ( current );
				
				// When the current node is equal to the end node, then we can break and return the path
				if ( current == end )
				{
					
					// Construct the shortest path
					while ( previous.ContainsKey ( current ) )
					{
						
						// Insert the node onto the final result
						graphPath.nodes.Insert ( 0, current );
						
						// Traverse from start to end
						current = previous [ current ];
					}
					
					// Insert the source onto the final result
					graphPath.nodes.Insert ( 0, current );
					break;
				}
				
				// Looping through the Node connections (neighbors) and where the connection (neighbor) is available at unvisited list
				for ( int i = 0; i < current.connections.Count; i++ )
				{
					City neighbor = current.connections [ i ] as City;
					
					// Getting the distance between the current node and the connection (neighbor)

					if (neighbor)
					{
						
					}
					
//					float length = Vector3.Distance ( current.transform.position, neighbor.transform.position );
					
					// The distance from start node to this connection (neighbor) of current node
					float alt = distances [ current ] + current.roads[i].distance;
					
					// A shorter path to the connection (neighbor) has been found
					if ( alt < distances [ neighbor ] )
					{
						distances [ neighbor ] = alt;
						previous [ neighbor ] = current;
					}
				}
			}
			graphPath.Bake ();
			return graphPath;
            
        }
    }
}