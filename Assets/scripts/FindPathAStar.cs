using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PathMarker
{
    public MapLocation location;
    public float G;
    public float H;
    public float F;
    public GameObject marker;
    public PathMarker parent;

    public PathMarker(MapLocation l, float g, float h, float f, 
        GameObject marker, PathMarker p)
    {
        location = l;
            G = g;
            H = h;
            F = f;
        this.marker = marker;
        parent = p;
    }

    public override bool Equals(object obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            return false;
        else 
            return location.Equals(((PathMarker)obj).location);
    }

    public override int GetHashCode()
    {
        return location.GetHashCode();
    }
}

public class FindPathAStar : MonoBehaviour
{
    public Maze maze;
    public Material closedMaterial;
    public Material openMaterial;

    List<PathMarker> open = new List<PathMarker>();
    List<PathMarker> closed = new List<PathMarker>();

    public GameObject start;
    public GameObject end;
    public GameObject pathPoint;

    PathMarker goalNode;
    PathMarker startNode;
    PathMarker lastPos;
    bool done = false;

    private void RemoveAllMarkers()
    {
        GameObject[] markers = GameObject.FindGameObjectsWithTag("marker");
        foreach(GameObject m in markers)
        {
            Destroy(m);
        }
    }

    private void BeginSearch()
    {
        done = false;
        RemoveAllMarkers();

        //Setup MapLocation thats not a wall
        List<MapLocation> locations = new List<MapLocation>();
        for(int z = 1; z < maze.depth - 1; z++)
        {
            for (int x = 1; x < maze.width - 1; x++)
            {
                //1 means wall so dont make any new location here
                if (maze.map[x,z] != 1)
                {
                    locations.Add(new MapLocation(x, z));
                }
            }
        }
        locations.Shuffle();

        //Setup Star and Goal Marker, maze.scale is pretty much the grid size
        Vector3 startLocation = new Vector3(locations[0].x * maze.scale, 0, locations[0].z * maze.scale);

        //Initial Distance Value
        float h0 = Vector2.Distance(
            new Vector2(locations[0].x, locations[0].z),
            new Vector2(locations[1].x, locations[1].z));
        startNode = new PathMarker(new MapLocation(locations[0].x, locations[0].z)
            ,0,h0,h0, Instantiate(start, startLocation, Quaternion.identity), null);



        Vector3 goallocation = new Vector3(locations[1].x * maze.scale, 0, locations[1].z * maze.scale);
        
        goalNode = new PathMarker(new MapLocation(locations[1].x, locations[1].z)
            , 0, 0, 0, Instantiate(end, goallocation, Quaternion.identity), null);


        //Starting node setup
        open.Clear();
        closed.Clear();
        open.Add(startNode);
        lastPos = startNode;
    }

    void Search(PathMarker thisNode)
    {
        if (thisNode == null)
        {
            return;
        }
        if (thisNode.Equals(goalNode))
        {
            done = true;
            return; //Goal has been found
        }

        foreach(MapLocation dir in maze.directions)
        {
            MapLocation neighbour = dir + thisNode.location;

            //Skip if neighbor is outside of the boundary of the maze
            if (neighbour.x < 1 || neighbour.x >= maze.width 
                || neighbour.z < 1 || neighbour.z >= maze.depth)
            {
                continue;
            }

            //remember 1 means wall so skip this
            if (maze.map[neighbour.x, neighbour.z] == 1)
            {
                continue;
            }

            //Skip if neighbour is already on the closed list
            if (IsOnClosedList(neighbour))
            {
                continue;
            }

            //Usually 10 for orthogonal movement, distance between one location to its neighbour
            //Will be added incrementally from the start to see how many location already traversed
            float G = Vector2.Distance(thisNode.location.ToVector(),
                neighbour.ToVector()) + thisNode.G;

            //General distance from the start node to the goal node
            float H = Vector2.Distance(neighbour.ToVector(), goalNode.location.ToVector());

            //F will be used to check which are the lowest to be added to the closed list
            float F = G + H;

            //pathBlock is not used to block player but as a visual representative of the F,G,H scores
            GameObject pathBlock = Instantiate(pathPoint, 
                new Vector3(neighbour.x * maze.scale, 0, neighbour.z * maze.scale), quaternion.identity);
            TextMesh[] values = pathBlock.GetComponentsInChildren<TextMesh>();
            values[0].text = "G: " + G.ToString("0.00");
            values[1].text = "H: " + H.ToString("0.00");
            values[1].text = "F: " + F.ToString("0.00");

            //When a node is sent to closed list, give information for its neighbor
            if (!UpdateMarker(neighbour, G, H, F, thisNode))
            {
                open.Add(new PathMarker(neighbour, G, H, F, pathBlock, thisNode));
            }
        }

        
        //Get the lowest member of the open list (the one with the lowest F)
        open = open.OrderBy(p => p.F).ThenBy(n => n.H).ToList<PathMarker>();
        PathMarker pm = (PathMarker)open.ElementAt(0);

        //Add it to closed list and remove from the open list
        closed.Add(pm);

        open.RemoveAt(0);
        pm.marker.GetComponent<Renderer>().material = closedMaterial;

        lastPos = pm;
    }

    private bool UpdateMarker(MapLocation pos, float g, float h, float f, PathMarker parent)
    {
        foreach(PathMarker p in open)
        {
            if (p.location.Equals(pos))
            {
                p.G = g;
                p.H = h;
                p.F = f;
                p.parent = parent;
                return true;
            }
        }
        return false;
    }

    private bool IsOnClosedList(MapLocation marker)
    {
        foreach(PathMarker p in closed)
        {
            if (p.location.Equals(marker))
            {
                return true;
            }
        }
        return false;
    }

    private void GetPath()
    {
        RemoveAllMarkers();
        PathMarker begin = lastPos;

        while (begin != null && !startNode.Equals(begin) && begin != null)
        {
            Instantiate(pathPoint, new Vector3(begin.location.x * maze.scale, 0,
                begin.location.z * maze.scale), quaternion.identity);
            begin = begin.parent;
        }

        Instantiate(pathPoint, new Vector3(startNode.location.x * maze.scale, 0,
            startNode.location.z * maze.scale), quaternion.identity);
    }

    private void Start()
    {
        
    }
    private void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            BeginSearch();
        }
        if (Keyboard.current.cKey.wasPressedThisFrame && !done)
        {
            Search(lastPos);
        }
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            GetPath();
        }
    }
}
