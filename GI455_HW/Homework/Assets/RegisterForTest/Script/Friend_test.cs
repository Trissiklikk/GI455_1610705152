using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend_test : MonoBehaviour
{
    public string data = "b,9,A,H,h,L,2,b,j,8,y,v,f,J,4,K,6,p,U,a,5,i,L,m,O,C,T,y,7,6,I,j,r,2,a,6,e,2,2,y,P,8,6,0,5,z,R,0,e,O,m,8,H,2,2,a,a,9,t,6,m,6,2,F,l,7,3,B,x,H,Z,o,Y,B,K,n,p,p,q,4,4,I,s,Z,8,m,j,e,8,1,B,W,G,1,E,t,x,r,8,9,s,4,6,x,a,y,m,N,K,s,4,4,b,q,Y,1,A,j,7,a,m,k,3,c,x,2,7,S,2,3,5,W,K,F,h,Z,P,J,7,N,0,8,w,c,U,z,w,X,w,J,L,O,e,g,o,X,8,o,C,X,o,N,k,h,8,0,2,3,q,1,3,y,L,3,V,x,u,3,4,t,r,6,o,m,S,c,8,4,C,w,p,F,R,Y,M,w,H,M,o,w,V,w,y,7,4,c,M,T,g,v,2,Q,7,A,0,7,z,h,1,2,z,1,v,o,4,U,0,M,k,A,m,3,k,5,a,3,5,C,o,C,9,O,9,S,f,B,H,C,G,5,t,f,J,6,h,0,9,1,W,X,I,6,j,2,8,8,U,j,f,v,2,L,j,X,c,6,1,n,8,1,P,p,J,v,2,c,8,5,9,8,t,4,O,z,H,M,9,9,6,G,1,E,G,G,a,w,N,0,J,s,1,e,J,o,8,M,4,N,T,0,1,3,8,2,i,u,f,x,O,0,d,a,o,2,L,r,X,c,V,b,M,S,8,T,S,Q,N,A,l,2,g,t,Q,0,n,H,O,d,l,g,x,7,t,y,6,6,q,w,y,X,q,4,a,g,Y,R,l,a,A,O,x,c,B,4,F,8,7,y,y,1,G,4,Z,5,7,b,7,n,4,M,m,F,Q,S,d,w,q,i,f,U,S,X,B,C,1,V,8,2,S,D,j,t,z,6,F,Z,9,X,w,E,1,2,R,m,U,r,3,0,S,8,c,f,C,p,V,T,2,3,e,e,h,T,q,8,z,O,1,k,A,1,Z,7,l,E,N,X,y,t,S,2,8,2,0,4,Y,e,7,y,k,K,u,v,8,p,8,B,U,n,3,E,K,0,5,R,3,R,N,M,D,W,O,f,w,M,4,a,M,4,7,O,F,p,H,j,3,v,k,Q,h,3,G,Y,s,K,B,h,V,m,p,1,u,K,b,6,4,n,X,5,5,6,Y,u,d,Y,6,F,1,I,t,L,9,3,a,3,e,B,6,H,L,C,C,d,Z,T,H,1,2,1,3,2,1,r,j,6,M,f,u,N,C,L,k,9,R,s,s,Q,R,2,D,q,Y,Y,6,O,p,T,2,X,k,u,K,R,6,f,v,S,k,X,E,J,M,o,0,c,x,m,o,Q,i,F,6,f,Q,Z,n,6,Y,q,l,r,f,C,9,8,P,h,X,f,g,p,p,6,H,0,B,F,7,5,0,z,K,B,O,U,X,i,a,S,e,F,n,W,3,R,X,Q,j,9,Z,7,7,H,m,7,M,H,8,W,I,p,o,S,z,i,l,s,l,y,Z,V,B,2,M,B,F,E,5,u,Y,G,A,i,f,O,N,J,4,H,S,3,L,s,7,Y,V,Z,4,0,1,4,e,6,3,a,G,M,3,N,2,u,I,u,2,U,1,4,x,5,i,F,l,3,q,o,Y,C,u,Z,9,r,g,f,d,O,9,3,j,2,p,4,6,4,t,s,M,I,4,C,r,8,x,V,V,Y,l,u,3,F,6,3,X,Y,M,g,1,p,U,k,3,3,d,n,3,8,Z,P,Q,f,c,R,X,M,i,0,K,V,V,0,3,g,b,5,A,a,7,p,N,c,S,7,2,w,i,b,f,8,2,G,W,C,1,l,L,5,3,S,g,L,j,d,l,1,B,3,4,P,a,V,T,a,P,T,o,5,G,Q,V,d,5,9,2,h,K,9,f,8,O,0,m,8,J,t,6,l,C,4,n,2,B,p,f,8,v,2,u,8,l,G,g,N,b,a,X,E,B,0,G,N,Z,T,2,0,9,a,z,Q,4,3,z,W,S,7,I,Z,2,2,O,M,N,r,P,o,A,4,y,7,h,I,K,z,Z,v,u,t,E,A,X,K,l,t,N,a,x,F,6,x,X,q,T,G,W,O,O,h,2,4,V,0,8,r,U,x,8,m,n,6,P,B,Y,U,Q,N,n,Q,O,E,i,V,I,y,A,W,4,O,O,h,8,9,B,N,i,U,1,3,V,1,s,1,8,0";
    public string[] splitArray;
    public string ansTest;



    public void Test()
    {
        //data = new string[] {"b,9,A,H,h,L,2,b,j,8,y,v,f,J,4,K,6,p,U,a,5,i,L,m,O,C,T,y,7,6,I,j,r,2,a,6,e,2,2,y,P,8,6,0,5,z,R,0,e,O,m,8,H,2,2,a,a,9,t,6,m,6,2,F,l,7,3,B,x,H,Z,o,Y,B,K,n,p,p,q,4,4,I,s,Z,8,m,j,e,8,1,B,W,G,1,E,t,x,r,8,9,s,4,6,x,a,y,m,N,K,s,4,4,b,q,Y,1,A,j,7,a,m,k,3,c,x,2,7,S,2,3,5,W,K,F,h,Z,P,J,7,N,0,8,w,c,U,z,w,X,w,J,L,O,e,g,o,X,8,o,C,X,o,N,k,h,8,0,2,3,q,1,3,y,L,3,V,x,u,3,4,t,r,6,o,m,S,c,8,4,C,w,p,F,R,Y,M,w,H,M,o,w,V,w,y,7,4,c,M,T,g,v,2,Q,7,A,0,7,z,h,1,2,z,1,v,o,4,U,0,M,k,A,m,3,k,5,a,3,5,C,o,C,9,O,9,S,f,B,H,C,G,5,t,f,J,6,h,0,9,1,W,X,I,6,j,2,8,8,U,j,f,v,2,L,j,X,c,6,1,n,8,1,P,p,J,v,2,c,8,5,9,8,t,4,O,z,H,M,9,9,6,G,1,E,G,G,a,w,N,0,J,s,1,e,J,o,8,M,4,N,T,0,1,3,8,2,i,u,f,x,O,0,d,a,o,2,L,r,X,c,V,b,M,S,8,T,S,Q,N,A,l,2,g,t,Q,0,n,H,O,d,l,g,x,7,t,y,6,6,q,w,y,X,q,4,a,g,Y,R,l,a,A,O,x,c,B,4,F,8,7,y,y,1,G,4,Z,5,7,b,7,n,4,M,m,F,Q,S,d,w,q,i,f,U,S,X,B,C,1,V,8,2,S,D,j,t,z,6,F,Z,9,X,w,E,1,2,R,m,U,r,3,0,S,8,c,f,C,p,V,T,2,3,e,e,h,T,q,8,z,O,1,k,A,1,Z,7,l,E,N,X,y,t,S,2,8,2,0,4,Y,e,7,y,k,K,u,v,8,p,8,B,U,n,3,E,K,0,5,R,3,R,N,M,D,W,O,f,w,M,4,a,M,4,7,O,F,p,H,j,3,v,k,Q,h,3,G,Y,s,K,B,h,V,m,p,1,u,K,b,6,4,n,X,5,5,6,Y,u,d,Y,6,F,1,I,t,L,9,3,a,3,e,B,6,H,L,C,C,d,Z,T,H,1,2,1,3,2,1,r,j,6,M,f,u,N,C,L,k,9,R,s,s,Q,R,2,D,q,Y,Y,6,O,p,T,2,X,k,u,K,R,6,f,v,S,k,X,E,J,M,o,0,c,x,m,o,Q,i,F,6,f,Q,Z,n,6,Y,q,l,r,f,C,9,8,P,h,X,f,g,p,p,6,H,0,B,F,7,5,0,z,K,B,O,U,X,i,a,S,e,F,n,W,3,R,X,Q,j,9,Z,7,7,H,m,7,M,H,8,W,I,p,o,S,z,i,l,s,l,y,Z,V,B,2,M,B,F,E,5,u,Y,G,A,i,f,O,N,J,4,H,S,3,L,s,7,Y,V,Z,4,0,1,4,e,6,3,a,G,M,3,N,2,u,I,u,2,U,1,4,x,5,i,F,l,3,q,o,Y,C,u,Z,9,r,g,f,d,O,9,3,j,2,p,4,6,4,t,s,M,I,4,C,r,8,x,V,V,Y,l,u,3,F,6,3,X,Y,M,g,1,p,U,k,3,3,d,n,3,8,Z,P,Q,f,c,R,X,M,i,0,K,V,V,0,3,g,b,5,A,a,7,p,N,c,S,7,2,w,i,b,f,8,2,G,W,C,1,l,L,5,3,S,g,L,j,d,l,1,B,3,4,P,a,V,T,a,P,T,o,5,G,Q,V,d,5,9,2,h,K,9,f,8,O,0,m,8,J,t,6,l,C,4,n,2,B,p,f,8,v,2,u,8,l,G,g,N,b,a,X,E,B,0,G,N,Z,T,2,0,9,a,z,Q,4,3,z,W,S,7,I,Z,2,2,O,M,N,r,P,o,A,4,y,7,h,I,K,z,Z,v,u,t,E,A,X,K,l,t,N,a,x,F,6,x,X,q,T,G,W,O,O,h,2,4,V,0,8,r,U,x,8,m,n,6,P,B,Y,U,Q,N,n,Q,O,E,i,V,I,y,A,W,4,O,O,h,8,9,B,N,i,U,1,3,V,1,s,1,8,0"};
        /*for (int i = 0; i < data.Length; i++)
        {
            string[] splitArray = data[i].Split(',');
            Debug.Log(splitArray);
        }*/



        for (int j = 0; j < splitArray.Length; j++)
        {
            if (j % 2 != 0)
            {
                ansTest = splitArray[j] + ",";
            }

        }
        Debug.Log(ansTest);
    }
}

    

