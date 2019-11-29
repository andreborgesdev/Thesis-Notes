# Introduction

## Node Chain

Node - The Node is the most basic building block for many common Data Structures. The Node fulfills two functions. The first is that it provides a mechanism to contain a piece of data. The second, is that it provides a means of connecting itself to other nodes via an object reference pointer. This object reference is known as the Next pointer. So, as you can see here, we have a single node. It has a value of 3 and a Next pointer, but it has nothing to point to. So, let's create another node, this time with the value 5. Now, let's connect the first node to the second node. We've now created a chain with two nodes and one link joining them. This process can be repeated for many more nodes.

Node chain - A chain of Nodes

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/The_Node.png?raw=true)

The key thing to understand at this point is that all we've done is allocated two independent nodes; there's no link between either node. So, what we need to do to create the link is set the Next pointer of the first node to be the second node, and in this way we've linked the first node and the second node into a chain. And as before, this whole process can be repeated again for a third node, and a fourth, and on, and on, and on.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Node_Chains.png?raw=true)

## LinkedList

- The Linked List is a single chain of nodes
- Have a well defined starting point known as the List Head (Head Pointer). The List will provide a Pointer to the first node in the list
- Expose a Pointer to the last node in the list, the Tail (Tail Pointer)
- Provide operations that allow the list items to be managed, searched, and enumerated
  - Add
  - Remove
  - Find
  - Enumerate

### Add Items

#### Add to Front

The most basic operation that can be performed is adding an item to the list.

Linked List is a single node chain with Head and Tail node pointers, both of which have initial values of Null.

The first step to adding the node is allocating the node. Adding this node to the Linked List means first pointing the Head and Tail pointers at this node. Since the list has only one item, the Head and Tail pointers will both refer to that same item. Now the Linked List has a node to start the rest of the chain. Adding the second node is basically the same operation. We allocate a new node and point the Head node to it, pointing its Next pointer to the node that was previously the Head node. Since we're adding the node to the start of the list, we don't need to update the Tail pointer, it already points to the last node.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/LinkedList_Add_To_Front.png?raw=true)

This is a very efficient operation. Adding a new node to the front of the list only involved allocating the data and updating a few pointers. ___The complexity or performance of this operation remains constant regardless of how many nodes are in the list.___

Now, if we were using an Array instead of Linked List, adding to the front would involve shifting all of the existing data to the right, and if the Array were full, it would involving allocating an entirely new Array, a larger Array, and copying all of the data from the smaller Array to the larger one.

This characteristic of efficient insertion is one of the key benefits of the Linked List Data Structure.

#### Add to End

It can be just as useful to add a node to the end of the list. Like before, we have an empty list with a Null Head and Tail pointers, and it's that Tail pointer that's going to make this operation much easier. When we create our first node, like before, the Head pointer is updated to point to the new node, and so is the Tail pointer. Now, when a second node is created, the Head pointer remains unchanged, and the Tail pointer points to the new node. Having the Tail pointer allows us to add the node to the tail very easily. It could certainly have been done without a Tail pointer, but nearly as easily or with such predictable performance characteristics.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/LinkedList_Add_To_End.png?raw=true)

### Remove Items

#### Remove from end

Like Add, Remove can be performed from the front or the end of the list.

Removing the Tail node 7 requires updating the Tail pointer and setting the next value for 5 to null; doing that will eliminate all references to the 7 node, which effectively removes it from the list.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/LinkedList_Remove_From_End.png?raw=true)

___This requires enumerating over all of the nodes in the list.___ This is because we're only storing references to the Head and Tail nodes. But, to perform this operation, we actually need to update the second to last node. Now, this is not incredibly complex. It's a simpler while loop to iterate until the second to last node. But having to walk the list every time a node is removed from the end can end up having significant performance implications on your application. Imagine a list that contained millions of nodes; removing each node from the end could require millions of millions of operations.

#### Remove from front

So, removing from the front of the list is not nearly as complex; it simply involves setting the Head pointer to the node that follows Head, and that's it. When the list contains only one node, removing it involves just setting Head and Tail to Null.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/LinkedList_Remove_From_Front.png?raw=true)