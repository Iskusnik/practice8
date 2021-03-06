﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice8
{
   
    class Line
    {
        public string A { get; set; }
        public string B { get; set; }
        public Line(string a, string b)
        {
            if (CompareStrings(a, b))
            {
                A = a;
                B = b;
            }
            else
            {
                B = a;
                A = b;
            }
        }
        public override int GetHashCode()
        {
            if ((A + B).GetHashCode() < 0)

                return (-(A + B).GetHashCode());
            else
                return ((A + B).GetHashCode());
        }
        public override bool Equals(object obj)
        {
            if (((Line)obj).GetHashCode() == this.GetHashCode())
                return true;
            else
                return false;
        }

        static public bool CompareStrings(string s1, string s2)//s1 < s2 = true
        {
            for (int i = 0; i < s1.Length; i++)
                if (i == s2.Length)
                    return false;
                else
                if (s1[i] < s2[i])
                    return true;

            return false;
        }
        public override string ToString()
        {
            return (A + B).ToString();
        }
    }
    class Node
    {
        public string NodeName { get; set; }
        public int Connections { get; set; }
        public Node[] ConnectionsInfo { get; set; }
        public int Visited { get; set; }
        public Node(string name)
        {
            NodeName = name;
            Connections = 0;
            ConnectionsInfo = new Node[1000];
            Visited = 0;
        }
        public override string ToString()
        {
            return NodeName;
        }
    }
    class Click
    {
        public Node Info { get; set; }
        public Click Next { get; set; }
        public Click (Node info)
        {
            Info = info;
            Next = null;
        }
    }
}
