# CVRP Problem 

Main program finds aproximation of solution of CVRP problem using one of four algorithms. 
GraphGenerator generates random graph that helps testing algorithms.

## How to use

### Algorithm types:
0 - greedy
1 - basic ACO
2 - local ACO
3 - elite ACO



MSI2

  -i, --input        Required. Input file
  
  -o, --output       Required. Output file
  
  -a, --algorithm    Required. Algorithm type
  
  -v, --vehicles     Required. Vehicles number
  
  -c, --capacity     Required. Capacity
  
  -s, --smax         Required. S Max
  
  
 --help             Display this help screen.
 
  --version          Display version information.



GraphGenerator

  -o, --output      Required. Output file
 
  -v, --vertives    Required. Number of vertices
 
  -d, --demand      Required. Max demand
 
  -w, --weight      Required. Max edge weight
 
  --help            Display this help screen.
 
  --version         Display version information.
