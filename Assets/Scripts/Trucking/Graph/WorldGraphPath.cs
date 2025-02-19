using System.Collections.Generic;
using Trucking.Model;

namespace Trucking.Graph
{
    public class WorldGraphPath : GraphPath
    {
        public override void Bake()
        {
            List<City> calculated = new List<City> ();
            m_Length = 0f;
            for ( int i = 0; i < m_Nodes.Count; i++ )
            {
                City node = m_Nodes [ i ] as City;
                for ( int j = 0; j < node.connections.Count; j++ )
                {
                    City connection = node.connections [ j ] as City;
				
                    // Don't calcualte calculated nodes
                    if ( m_Nodes.Contains ( connection ) && !calculated.Contains ( connection ) )
                    {
                        m_Length += node.roads[j].distance;
                    }
                }
                
                calculated.Add ( node );
            }
        }
    }
}