using System;
using System.Collections.Generic;
using System.Linq;

namespace All_O_one_Data_Structure
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
    }
  }

  public class AllOne
  {
    // linked list of nodes , each node will have set of keys(HashSet<string>) and frequency(int), all keys having same frequency would be in same list node.
    // linked list nodes frequency would be in increasing order
    // when we increment key's value ----- 
      // a new node will be added ---
        // if not present in next of current
        // OR
        // if the next node is present but the next node frequency is bigger than the frequency we have got after increment the current key value.
        // update in map current key's new linked list Node.
        // from current node keys remove the key value.
      // if the next node in which we can add the current key after increment
        // next nodes keys list add the current key
        // update in map current key's new linked list Node.
      // after removing the key from current node key's list, if the count became 0, remove the node from linked list 
    // similarly when we decrease the frequency
    LinkedList<(HashSet<string> keys, int frequency)> ll;
    Dictionary<string, LinkedListNode<(HashSet<string> keys, int frequency)>> map;
    public AllOne()
    {
      ll = new LinkedList<(HashSet<string> keys, int frequency)>();
      map = new Dictionary<string, LinkedListNode<(HashSet<string> keys, int frequency)>>();
    }

    public void Inc(string key)
    {
      // When its a new Key
      if (!map.ContainsKey(key))
      {
        LinkedListNode<(HashSet<string> keys, int frequency)> newNode = null;
        // when the ll is empty
        if (ll.Count == 0)
        {
          newNode = ll.AddFirst((new HashSet<string>(), 1));
        }
        else
        {
          // if the first node of the ll having frequency 1
          // new key will be added as a frequency 1
          if (ll.First.Value.frequency == 1)
          {
            // new key can be added in the ll first node as it has having frequncy 1
            newNode = ll.First;
          }
          else
          {
            // if the first node frequncy is not 1, then will be adding a node at the first with frequency 1.
            newNode = ll.AddFirst((new HashSet<string>(), 1));
          }
        }
        // add the key in the keys list.
        newNode.Value.keys.Add(key);
        // update the map for the current key
        map[key] = newNode;
        return;
      }

      // when its an existing key
      var existingNode = map[key];
      // get the next node
      var next = existingNode.Next;
      // if the next node is not present OR next node frequnecy is greater. 
      if (next == null || next.Value.frequency > existingNode.Value.frequency + 1)
      {
        // will be adding a new node after the current node by incrementing the frequency by 1 of current node frequncy
        next = ll.AddAfter(existingNode, (new HashSet<string>(), existingNode.Value.frequency + 1));
      }
      // in case the next node is the node where the new key can be added
      next.Value.keys.Add(key);
      // update the map
      map[key] = next;

      // remove the key from the current node
      existingNode.Value.keys.Remove(key);
      // after removal of key if the keys count is 0 for the current node, remove that also
      if (existingNode.Value.keys.Count == 0)
      {
        ll.Remove(existingNode);
      }
    }

    public void Dec(string key)
    {
      // if the key is not present
      if (!map.ContainsKey(key) || map[key] == null || map[key].Value.keys.Count == 0) return;

      // get the node
      var existingNode = map[key];
      // get the previous node
      var previous = existingNode.Previous;
      // if the current key frequency is more than 1
      if (existingNode.Value.frequency != 1)
      {
        // if no node available before current node OR previous node frequncy is lesser after reducing 1 from the current frequency 
        if (previous == null || previous.Value.frequency < existingNode.Value.frequency - 1)
        {
          // need to add a new node before the current node with frequncy -1 of current frequency
          previous = ll.AddBefore(existingNode, (new HashSet<string>(), existingNode.Value.frequency - 1));
        }

        // in case the previous node is the node which frequncy is matched after reducing current frequncy
        // add the key in previous node
        previous.Value.keys.Add(key);
        // udpate the map
        map[key] = previous;
      }
      else
      {
        // if the current key frequncy is 1, after decrement it will be 0, as the frequency is 0 we can remove the key from the map.
        map.Remove(key);
      }

      existingNode.Value.keys.Remove(key);
      if (existingNode.Value.keys.Count == 0)
      {
        ll.Remove(existingNode);
      }
    }

    public string GetMaxKey()
    {
      if(ll.Count != 0)
      {
        return ll.Last.Value.keys.First();
      }

      return string.Empty;
    }

    public string GetMinKey()
    {
      if (ll.Count != 0)
      {
        return ll.First.Value.keys.First();
      }

      return string.Empty;
    }
  }
}
