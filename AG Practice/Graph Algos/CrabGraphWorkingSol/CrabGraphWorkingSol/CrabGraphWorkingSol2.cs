#include <cstring>
#include <iostream>
#include <vector>
#include <cstdio>
#include <algorithm>
using namespace std;

// the maximum number of vertices
#define NN 300
#define INF 10000000

// adjacency matrix (fill this up)
int cap[NN][NN];

// flow network
int fnet[NN][NN];

// BFS
int q[NN], qf, qb, prv[NN];

int fordFulkerson( int n, int s, int t )
{
    // init the flow network
    memset( fnet, 0, sizeof( fnet ) );

    int flow = 0;

    while( true )
    {
        // find an augmenting path
        memset( prv, -1, sizeof( prv ) );
        qf = qb = 0;
        prv[q[qb++] = s] = -2;
        while( qb > qf && prv[t] == -1 )
            for( int u = q[qf++], v = 0; v < n; v++ )
                if( prv[v] == -1 && fnet[u][v] - fnet[v][u] < cap[u][v] )
                    prv[q[qb++] = v] = u;

        // see if we're done
        if( prv[t] == -1 ) break;

        // get the bottleneck capacity
        int bot = 0x7FFFFFFF;
        for( int v = t, u = prv[v]; u >= 0; v = u, u = prv[v] )
            bot = min(bot, cap[u][v] - fnet[u][v] + fnet[v][u]);

        // update the flow network
        for( int v = t, u = prv[v]; u >= 0; v = u, u = prv[v] )
            fnet[u][v] += bot;

        flow += bot;
    }

    return flow;
}

#define A(x) ((x) * 2 + 2)
#define B(x) ((x) * 2 + 3)
#define source 0
#define target 1

int c, n, m, t, a, b;

int main() {
    for(cin >> c; c--; ) {
        cin >> n >> t >> m;
        //cerr << n << " " << t << " " << m << endl;
        memset(cap, 0, sizeof cap);
        for(int i = 0; i < m; i ++) {
            cin >> a >> b; a--; b--;
            cap[A(a)][B(b)] = INF;
            cap[A(b)][B(a)] = INF;
        }
        for(int i = 0; i < n; i ++) {
            cap[source][A(i)] = t;
            cap[B(i)][target] = 1;
        }
        int result = fordFulkerson(2 * n + 2, source, target);
        cout << result << endl;
    }
    return 0;
}
