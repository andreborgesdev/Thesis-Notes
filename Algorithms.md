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

### Enumerate

Enumerating over the nodes is not difficult, but is done so frequently that it's worth looking at it in more detail. The key to enumerating over a Linked List is keeping a pointer to the next node to enumerate. Eventually, the Tail node is reached, and after yielding the value, current is assigned to null, which causes the loop to terminate and the method to return.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/LinkedList_Enumerate.png?raw=true)

### Singly Linked List

this is a somewhat simplistic implementation; it doesn't take into account a lot of the error handling cases that a production class would need, and it only implements a subset of the truly useful behaviors that a Linked List offers, but it gives enough insight into how the Linked List works that I think it's a good foundation to work from. 

___Linked List Node___ is very similar to the node class that we've already looked at several times in the module so far, only this time, you can see that the Linked List node takes advantage of the C# generic syntax. This allows us to hold not just integers but any type of object within our Linked List.

One more deviation from what we've seen is that we've added a constructor, and this constructor allows us to create a node with a pre-specified value. This is just a little bit of syntax sugar to help with creating nodes. Otherwise, the node is nearly identical to what we saw before.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/LinkedList_Implementation.png?raw=true)

___Linked List Class___ is also a generic type, meaning that the Linked List can contain nodes that hold any type of data, and it implements the ICollection<T> generic interface, and this interface provides a certain set of operations that all collections that implement this interface are guaranteed to expose.

We have the pointer to the Head node and the pointer to the Tail node. These are auto-implemented properties, so they default to the value null, they have public getters but private setters, because the list should be managing these values and nothing else.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/LinkedList_Class.png?raw=true)

For the ___ICollection interface___ we have the Count property, another auto-implemented property with a public getter and a private setter. At any moment, the Count property will indicate the number of nodes that are currently in the list. It contains opertaions like:

- Add() - Adds the item to the front of the list
- Contains - performs a simple enumeration over the list and returns true if a node with the specified value is found. This enumeration pattern matches what the GetEnumerator does, only it puts the behavior in line with the enumeration
- CopyTo - copy all of the nodes from the Linked List into an Array, and again, it's the basic enumeration pattern.
- IsReadOnly
- Remove - this one differs from the Removes we've looked at is that it doesn't require that the item be removed from the start or the end of the list, it allows an item by value to be removed anywhere within the list. There are four cases we need to care about here. We have an Empty list where nothing's done; we have a list with a Single node where we just want to remove that node if the value matches; and we have a list with Many nodes. And within the Many nodes, we distinguish between removing the first node in the list and removing any subsequent node in the list.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/LinkedList_ICollection_Remove.png?raw=true)

- GetEnumerator<T>
- GetEnumerator
- Clear - Clear in a Linked List is very simple in a garbage collected environment; you just set Head to null, Tail to null, and Count to 0. We can do this in C#, because what that will do is remove all of our references to every node in the node chain, and they'll be garbage collected.

### Doubly Linked List

What we've looked at so far is known as a Singly Linked List. Now, it's called that because each node in the chain has a single link to another node. This type of Linked List works great when you only need forward access to the nodes. But, there are times when being able to enumerate the nodes both forwards and backwards might be beneficial. There's a specialization of the Linked List, where each node contains two pointers; one to the Next node just like the Singly Linked List, and one to the Previous node.

A Doubly Linked List starts with the single node. In this case, we have a node with a value 3, and Null Previous and Next pointers. Just like with the Singly Linked List, we need to create a second node to start the node chain. So, we'll create a node with the value 5, and it also has Null Previous and Next pointers. Now, what we're going to need to do is create the chain by linking these two nodes together. Now, like the Singly Linked List, the Next pointer of the first node will now point to the second node. But because this is a Doubly Linked List, we'll also create a link back from the second node to the first. This means we can navigate from the second node back to the first node as easily as we can the first to the second. And just like a Singly Linked List, this pattern can continue for as many nodes as needed. Now, a Singly Linked List and a Doubly Linked List are very similar in implementation.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/DoublyLinkedList.png?raw=true)

In the ___Linked List Node___ we only add the pointer to the Previous Node.

The ___Doubly Linked List Class___ is also nearly identical to the Singly Linked List class. The differences are around managing the Previous pointer

The differences are:

- AddFirst()
- AddLast()
- RemoveFirst()
- RemoveLast() - Here we see performace improvment because we don't have to iterate through the entire list
- Remove() 

For the cost of one additional pointer in the node, and a little more pointer management during Add and Remove operations, we were able to remove the most expensive operation that existed within the Linked List class, and that's a really good performance benefit to take for a little bit of overhead, and it also makes the list much more functional. Now as a consumer, for-each will only iterate forwards in the list, that hasn't changed, but once I'm midway through the list, I can manually walk backwards, and that's a very powerful pattern to be able to have introduced.

The Doubly Linked List is so important, that. NET's own Linked List class is a Doubly Linked List.

### Modern Implementations
