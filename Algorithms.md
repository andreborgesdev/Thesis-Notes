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

In the real world, however, you would be unlikely to come across many situations where you would create your own Linked List structure. Typically, you'll be using a language or framework that provides a Linked List class that is already tested, performant, and compatible with your application. Unless you have a proven need to create your own Linked List, I would highly recommend that you use the one provided for you.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/LinkedList_Class_DOTNET.png?raw=true)

## Stacks

A Stack is a collection in which data is added and removed in a Last In First Out order. This means that the item most recently added to the Stack will be the one that is next handed back from the collection.

The restaurant plate stacker is one of the easiest way to explain how a Stack works. This is a device where clean plates are put into a spring-loaded cylinder. As customers need a clean plate they walk up to the stacker and remove the top plate.

### Push

So we have an empty plate stacker. Since it's empty, the dishwasher is notified that more plates are needed, and as plates are cleaned, they're added to the stack. In computer science terms, this is known as "pushing".

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Stack_Push.png?raw=true)

### Pop

Now, at this point, we have a plate stacker with four plates in it. A customer could walk up and could see that there is at least one plate in the stacker. She doesn't necessarily know or even care how many plates are beneath that plate; all she really cares about is that there's one available for her. So, she can take the top plate from the stack. In computer science terms, this action is known as "popping" the plate from the stack.

Last In First Out means the last plate added to the stack was the first plate removed by the customer. As more and more customers come, the plates are popped from the stack, each time reducing the depth or the number of items in the stack. Eventually, the stack is empty, and at this point, no more items can be popped from the stack.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Stack_Pop.png?raw=true)

### Using a Linked List

A Linked List is a series of nodes linked together into a chain. We can use this type of structure to help build our stack. The stack would contain a Linked List whose Head node pointed to the most recently added or top item.

Eventually, the stack owner will need to remove data, and will do so by calling the pop method. When this happens, the front node of the list is removed, and the next node of the node chain becomes the top of the stack.

So, ___why would I choose to use a Linked List___ as the data storage medium for a stack as opposed to another structure such as an Array? Well first, because it's a Linked List, it has no hard size limit; the only real constraint is the number of nodes that can be allocated, and for modern personal computers this would be a huge number, millions or billions of nodes. It's also a very straightforward way to implement a stack. An Array, for example, would require bounds checking to make sure that a push didn't cause the item to exceed the Array's bounds.

But there are ___downsides___ to this approach, and many of them have to do with performance and scalability. The first is that adding an item to the stack causes a memory allocation to occur every time. This can end up causing undesirable performance characteristics in high performance systems. On a related issue, the memory cost for each node can be significantly more than the cost of the data being stored. For example, a 32-bit value such as an integer might have memory overhead several times larger than the integer itself. And then there's the bucket of general performance issues. These issues really only affect high performance systems, but they can be very significant. They include things like data locality problems and memory fragmentation; things that Linked Lists run into because they're storing the nodes throughout the heap, but an Array can avoid by keeping all the data really near each other.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Stack_LinkedList.png?raw=true)

We've seen how a Linked List makes it very easy to implement a Stack, but does so at the cost of performance.

### Array

Now we will use an Array as the storage mechanism.

When this example starts, there's no Array because the Stack is empty, but eventually, a value is pushed onto the Stack. When this happens, the Stack allocates an Array. Now notice that even though a single item was pushed, the Array contains six open slots. This over-allocation of the array will reduce the number of times the array needs to grow as more items are pushed to the Stack. The pushed item is added to the Array in the first slot, and the Stack records that this Array index contains the top of the Stack. When another item is pushed onto the Stack, the item already in the Array remains in the same place; the new item is added after it, and the Top pointer is updated; this can continue over and over. And now because there is data at the top of the Stack, the item at the top can be peeked at, and as items are popped, the pointer to the top item is updated, and this can continue for as long as the Stack is not empty.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Stack_Array.png?raw=true)

Using an Array as the backing store  is a much more complex class.

Allocating an Array with a length of 0  is to avoid having to check for null later on. We can just check for length and find we have a length of 0. This makes the code a little more streamlined and avoids a special case.

Size is not equal to the number of items that can be held by the Array or the Array's length; this is the number of items currently in the Array.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Stack_Array_Push.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Stack_Array_Pop.png?raw=true)

Clear sets size to 0. Now one thing to keep in mind, which I didn't deal with in this class, is that setting size to 0 doesn't clear out the Array. Now, for integers that's fine, but if this Array contained disposable objects or objects that had finalizers, leaving them in that Array would keep a reference to them alive. So if you're implementing a production quality Stack, you're going to need to deal with issues like that, and this is one of the reasons that it's important to always consider using the Stack provided to you by the platform you're on, because it should be taking care of these issues for you.

For Enumerators we're using the yield syntax as we enumerate over the items; the only tricky thing here is we enumerate the items backwards, and that's because the last item in the Array is the first item we want to return. We want to return these items as if you had called Pop, Pop, Pop, Pop, Pop. If we return them from index 0 up to size, they'll be returned in First In First Out order not Last In First Out order.

___It should be clear that that complexity brings with it some performance improvements that are well worth it.___

### Postfix Calculator

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Stack_Postfix_Calculator.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Stack_Postfix_Calculator_Algorithm.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Stack_Postfix_Calculator_Algorithm_Code.png?raw=true)

Whether the Stack was backed by an Array or a Linked List or some other structure it didn't matter. It was the Last In First Out behavior of the Stack that made this possible.

### Undo

A Stack can be used to store these operations in a Last In First Out order, and how that can be used to undo operations.

### Modern Implementations

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Stack_Modern_Implementations.png?raw=true)

In the. NET framework, Peek returns you the value at the top of the Stack, and Pop returns you the value at the top of the Stack and removes that item from the Stack, but in the C++ Stack, Pop does not return the item, it simply removes the item, so you can see in the code, when the Stack is allocated and then items are pushed, we don't pop those items off, we look at the top item to retrieve the value, and then we pop the value off.

## Queue

First In First Out

In its simplest form, a Queue is a collection that returns items in the same order that they were added. You can think of this as a checkout line at a grocery store.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Queue.png?raw=true)

And what's nice about storing these items in a Queue is that even though we're not processing them quite as a fast as they come in, we're also not losing the values. And this is one of the reasons Queues are so popular in computer science; they're a structure that allows us to take incoming data and store it in a way that allows us to process it later, but in the order it showed up, which is a sort of fairness that you often look for in computer science. So eventually we process all the items and the Queue is empty.

### Enqueue

It all starts by creating the Queue.

Next we add or Enqueue an item. This is like a shopper getting into the grocery line. What we're going to do is add the value 1 into the Queue. Now that this item has been added, it is the head of the Queue.

Just like the Stack Data Structure we saw in a previous module, it can be useful to know what item is at the head of the Queue, and we can use the Peek method for that, and it returns the value 1, because that is the front or the head of the Queue.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Queue_Enqueue.png?raw=true)

### Dequeue

When we begin, the head of the Queue is at the number 1; that's the first item we added to the Queue. When the Dequeue operation starts, it removes the 1 from the Queue, returns it to the caller, and adjusts the head of the Queue to be the next oldest item or the number 2.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Queue_Dequeue.png?raw=true)

### Linked List Implementation

Implementing a Stack using a Linked List was quite simple, but that the simplicity came at a performance tradeoff. The Queue is no exception in this regard.

Each time we remove an item, the Head pointer moves to the next item in the list, but the Head of the list is always the Head of the Queue.

Why AddLast and RemoveFirst? Why not AddFirst and RemoveLast? Like the Stack, all of the items in the Queue can be enumerated over. The expectation of the caller would be that the items would be enumerated in the same order that they would be Dequeued. So, in the example we just saw, we would expect the enumeration to have worked 1, 2, 3, 4, 5. So, by storing them in the list in the Head to Tail order, we can use the list to perform the enumeration for us; it just works.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Queue_LinkedList.png?raw=true)

Enqueue simply adds the item provided to the end of the list. It doesn't have to worry about whether the list is empty or what other items might be in the list. The item being added should simply go to the end of the list.

Dequeue will simply take the first item in the list, store it off into a temporary variable, remove the first item, and then return the stored value. So, it's pulling off the first item in the list and returning it. If the list is empty an exception is thrown.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Queue_LinkedList_Enq_Deq.png?raw=true)

Because our type is enumerable we provide the Enumerator methods, and all we need to do is defer to the list. Now, being able to defer to the list is only possible because we're storing these items in first to last order; we're adding the items to the end of the list, which means the value that we want to enumerate first is the front of the list, and that's exactly how a list enumerator works, and that's why we store items in that order.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Queue_LinkedList_Enum.png?raw=true)

### Array Implementation

This is quite a bit more complex.

When we allocate the Queue, the backing store is an empty Array with five slots. Now, as items are Enqueued, they're added to the Array from the front to the end. Now, at this point, the Queue has five items in it, and the Array is full. So, if Dequeue were called, the first item would be removed. This has opened up a slot in the Array. A second call to Dequeue would free another slot. And what happens if we call Enqueue again? Well, the value is added to the first open slot.

Now we have a Queue whose Array has four of five slots filled. The Head item is the third item; that is, it's the next item that will be returned when Dequeue is called. The Tail item is the first Array item; that is the last item in the Queue. There's also an open slot in the Array. That slot will be filled the next time Enqueue is called.

So, the question now, with the Array being full, is what should happen if Enqueue is called yet again? Should an exception be thrown, or should the Array grow to accommodate more items?

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Queue_Array.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Queue_Array_Growth.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Queue_Array_Growth_2.png?raw=true)

Array growth is quite a bit more complex. It requires memory allocations, and it requires copying data around in a nontrivial manner. But, the performance benefits of using an Array, as they were with the Stack, are somewhat significant; all the items are kept together closer in memory, the allocations are reduced to periods where the Array is growing, and Enqueuing or Dequeuing items from the Queue requires only setting an Array value and modifying some indexes.

An Array of 0 items for the Queue, and this means I don't need to do null checks later on, and I can just do length checks each time I'm adding.

Linked Lists don't need to worry about wrapping or efficient storage. We don't want to waste space that we've allocated, so we need to do the extra work to make sure we make the best use of it.

- Enqueue needs more work
- Dequeue is a bit more easy because it does not need to care about array growth
- Enumerate needs more work as well, same logic as Enqueue growth loop

Linked List implementation, but it comes with the benefits that Arrays have over Linked Lists, which include data locality and performance gains, as well as reducing the overall number of allocations and incredibly fast Enqueue and Dequeue times when there isn't an allocation being performed. Array back stores are quite popular, and in fact are the default for many Queue classes implemented in the real world.

### Priority Queue

Queues show up in software design very frequently, and quite often they're implemented as a specialization of the Queue, known as a Priority Queue. Priority Queues differ from normal Queues in that they are not First In, First Out, but rather, they return the highest priority items first, regardless of the order in which they were added to the Queue.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Queue_Priority.png?raw=true)

A good analogy might be a police station call center. People call the police for all sorts of reasons, and each reason has a specific priority. Life and death issues are a higher priority than complaints about noisy neighbors. So when the officer starts his shift, he has nothing in his Queue. Eventually, a call comes in with a noise complaint. Since it's the first call of the day, it's immediately the highest priority item in the officer's Queue. So, the officer begins the trip to the area of the complaint, but before he arrives, a higher priority incident occurs, an auto accident, and this now becomes his focus and the noise complaint falls further in the Stack. While still dealing with the auto accident, a call comes in from a store that has experienced a theft. This is more important than a noise complaint, but not as important as the auto accident. Finally, another noise complaint comes in, and being of equal priority to the existing noise complaint, it's added to the Queue at the same location.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Queue_Priority_Enqueue.png?raw=true)

### Modern Implementations

Queues are a commonly used Data Structure and one that has a place in every developer's tool belt. In fact, implementations of Queues exist in most modern languages and frameworks.

## Binary Trees

### Tree

Trees are similar to Linked Lists in that they chain together nodes of data, but they do it in a hierarchical rather than in a linear manner.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Tree.png?raw=true)

The Vice President has three direct reports, two Team Leads, and a Consultant. Each of these reporting relationships is represented by a line, and each of those lines represents a link in a unique chain between the Vice President and the subordinate.

There are a few properties to this Tree that are important to discuss. Let's start by sharing some common terms.

- In this Tree, the Vice President is the root or the top node; it's sometimes referred to as the Head node.
- The Consultant, and all of the unnamed team members, are said to be leaf nodes or terminal nodes.
- The Team Leads and the Consultant are child nodes or children to the Vice President, and the Vice President is said to be their parent. So, each Team Lead also has children and is itself their parent.

Now, let's talk about the relationship between the nodes. Each node is capable of linking itself to any arbitrary number of children. You can see the Vice President has three children, each Team Lead has four, and the Consultant has none. There's no inherent limit in this example to the number of children that a node could contain. The limit that does exist is that each node has exactly one parent. Now this causes a very important implication, and that is that there is exactly one path from the Vice President to any other node in the Tree, and likewise exactly one path from any node in the Tree back to the Vice President, and therefore, there is exactly one path that can be taken between any nodes in the Tree. That limitation, a single path between nodes, is a fundamental rule that the Tree structures we'll be looking at will never violate.

### Binary Trees

A Binary Tree is a Hierarchy of Data with some structure rules. It starts out with a Root Node. This is a node that has no parent, and at the moment it has no children. Now we can create zero, one, or two children. In this example, we now have two children; we have a Left Child, and we have a Right Child. Now, each child is itself a tree with the exact same structure limits as the parent. In this case we're showing Left Children and Right Children. So, unlike the previous Tree structure, the Binary Tree has at most two child nodes, thus the name Binary, and those children are known as the Left and the Right Children.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Tree_2.png?raw=true)

A Binary Search Tree doesn't change the structural rules of the Binary Tree, but it imposes an additional data rule, and that is that all the values in the Tree are stored in Sort Order; the smallest values are on the left, and the largest values are on the right.

We start with the Root Node. In this case, the node has the value 4. The Root Node has a Child with a value 2. Because the value is less than 4, it becomes the Left Child of the 4 node. The Root Node also has a Child with the value 6. Because this value is greater than 4, it becomes the Right Child of the Root Node. And this simple set of rules is followed recursively throughout the Tree. And now once the structure is created, we can see that it's sorted in a way that the left-most node contains the smallest value, and the right-most node contains the largest.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Search_Tree.png?raw=true)

### Adding Data

Adding items to the Tree is performed with a recursive algorithm. Now, throughout the module, we'll see that Trees lend themselves well to recursive algorithms, and that this can help use ease our understanding of Trees.

What if we add the value 4 when we already have the value 4 in the Tree? Well, we're going to treat it as a Larger Value. Now, that's a little bit odd that we would treat 4 as larger than 4, but ultimately we only have two choices; we can either send the node to the left or to the right, and that will be determined by whether we view it as being larger or smaller. So, we're going to view it as being larger. So, the 4 will go down to the 6, where it's determined that it's smaller than 6, so it will go to the left.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Search_Tree_Adding_Data.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Search_Tree_Add.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Search_Tree_AddTo.png?raw=true)

### Searching Data

Now what we're going to do is see why those data ordering requirements make the Binary Search Tree a really efficient structure for searching for data.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Search_Tree_Searching.png?raw=true)

On the left, we have the algorithm for finding data within the Tree, and on the right we have the Tree we're going to be searching.

Notice that we did find our Node only having to look at three nodes. Now imagine in a Linked List we could potentially have had to searched all of the items in the list.

7 doesn't have a right node, so current. Right from the 7 node is null, so once we recursively call Find, well the current node is null so we return null, and ultimately the caller of Find will get a null node value back, and that will inform the caller than no value was found in the Tree.

But even here notice in the case where the value did not exist in the Tree, we did not have to look at every node in the Tree to determine that; we only had to look at three of the nodes, and that's the property that makes a Binary Search Tree desirable compared to a structure like a Linked List. In a Linked List, the only way to determine that the value of 8 was not in the Linked List would have been to have looked at every node in the list.

So here we've been able to determine that nodes were or were not in the Tree while looking at a subset of the data in the Tree, and that's a very powerful mechanism, and one of the reasons that Binary Trees and similar Tree structures are very commonly used in computer science.

### Removing Data

Remove has three distinct phases

- First, we need to find the node to be deleted. If the node does not exist, we exit. Now, remember from Find, that it's easy to tell if a node does or doesn't exist in a Tree, and it's pretty efficient to do that.
- Now, if the node that we found is a terminal node or it's a leaf node, you simply remove it from the Tree by nulling out the parent's pointer to the node we're deleting. Now, a key point here is when we find that node we need to know its parent.
- And for a non-leaf node, what we need to do is find the correct child to replace the node we're deleting.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Search_Tree_Remove.png?raw=true)

Now, the 8 node has no right child, so what we're going to do is promote its left child into its place. Now, I want you to think about why that's the case. Since there's no child to the right, there is no value underneath the 4 that is going to be larger than 8. So, when we perform the delete operation, we've now promoted that Tree, the 6, 5, 7 nodes, up in place where the 8 was. And this works because we haven't broken the invariant structure of the tree. Everything to the right of the Root Node is greater than the Root Node, and when you go down to the 6 node, everything to its right is greater than 6, everything that's left is less than 6. So, we've been able to retain the rules that the Tree must follow while only having to move one block of nodes around.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Search_Tree_Remove_Case_1.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Search_Tree_Remove_Case_1_2.png?raw=true)

The node to the right has no left child, the 7 has no left, so we're going to promote that right child, the 7 node, up to where the 6 node is. And now this is going to mean not just re-pointing 4 to 7, but 7 also has to become the parent of 5. So what we did there was we moved that entire right Tree up into the Remove node slot. And again, this works because we haven't broken the invariant structure of the tree. We know that everything to the right of 7 is going to be greater than 7, and everything to the left will be less than 7.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Search_Tree_Remove_Case_2.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Search_Tree_Remove_Case_2_2.png?raw=true)

What we're going to do, is the right child's left-most child node will replace the removed node.

And now notice we've retained the invariant structure. Everything greater than 7 is to the right, everything less than 7 is to the left. And the reason we use the left-most child is because we know the left-most child is going to be the smallest value in the Tree.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Search_Tree_Remove_Case_3.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Search_Tree_Remove_Case_3_2.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Search_Tree_Remove_Code.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Search_Tree_Remove_Code_2.png?raw=true)

### Traversals

For a Linked List, we could enumerate from left to right or right to left. For a Stack, we would enumerate in a Last In, First Out order, and for a Queue in a First In, First Out order. But for a Tree it's not quite as obvious what to do, and that's because Trees have multiple links, potentially at every node. So, when you're at that node, should we go left first, should we go right first? And really you don't need to pick one over the other, you can pick all kinds of varieties.

The basic algorithm is you process the node that you're on, and then you visit the Left node, and then you visit the Right node, and at each of those nodes you follow that same algorithm; process the node you're on; this is the left, this is the right. What varies between the different algorithms is the order in which we do those three steps, and the three common orders that we're going to look at are Pre-Order, In-Order, and Post-Order.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Search_Tree_Traversals.png?raw=true)

And this might look somewhat random to you, but what's important is that the enumeration order was stable. We could enumerate this Tree using Pre-Order Traversal a thousand times, and each time it will enumerate in this order.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Search_Tree_Pre-Order_Traversal.png?raw=true)

In this case, we've been able to print out the Tree or process the Tree in Sort Order. The Tree is not stored 1, 2, 3, 4, 5, 6, 7, but we're able to process the nodes in that order. So, an In-Order Traversal is very commonly used when you have a Tree and you want to look at the items in Sort Order.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Search_Tree_In-Order_Traversal.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Search_Tree_Post-Order_Traversal.png?raw=true)

Both Pre-Order and Post-Order do have well-defined uses. They're often used in mathematical expression evaluation, and they're also quite often used when you're doing evaluation of runtime behaviors in a language. For example, compilers use Trees quite heavily, and Traversals dictate which operations happen in what order, and what operations depend on other operations. So, if you think of this as a dependency graph, what we're saying is, Step 1 and Step 3 have to operate before Step 2, and Step 5 and Step 7 have to operate before Step 6, and then Step 4 is the last one to operate because it's the parent; it's operating on the sum of the operations of all of its children.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Search_Tree_Pre-Post-Traversal.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Search_Tree_In-Order.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Binary_Search_Tree_In-Order_2.png?raw=true)

### Code

Contains is going to call a helper function called FindWithParent, and the reason we're calling FindWithParent, is so that when we get the value back we also get the parent node back, and this is really important, because remember that when we were doing Remove, Remove needs to know the parent of the node we're removing so that it can properly readjust the links. So, instead of having Contains operate differently than Remove, and instead of re-duplicating that code, I've just pulled out this helper method called FindWithParent, and Contains just ignores the parent, and if the value is not null when it returns, they contain nothing.

show an example that wasn't recursive as well, because as great as recursive algorithms are, they have problems and they have limitations, and if you have a tree that had a million nodes, are you really going to call this recursively in a pathologically bad case potentially a million times? You know, you'd run out of call Stack, you'd crash your application; we don't want to do that, and no production quality Tree should do that. No production quality Tree should use this recursive algorithm.

We make algorithms non-recursive using a Stack.

And also think about why this is a good thing. You know your Stack can hold a million items in it. And that's going to have memory constraints, and you know, there certainly are issues that might happen there, but you're not going to have the call stack problems that the runtime would have if you tried to make a million recursive calls. So, you've shifted the problem from a runtime call Stack depth problem to a memory constraint problem, and that's one that people tend to be able to understand and handle a little more gracefully.

### Sort

And this is a great example of where a Binary Search Tree does pretty well. It's able to take our input, and without a whole lot of effort, keep it in assorted order for us.

## Hash Tables

Hash Tables are a type of Data Structure that implements an Associative Array. Associative Arrays provide the Storage of Key/Value pairs into an Array or an Array-like collection, but unlike an Array, the index can be any comparable type, not just an integer. So, besides being comparable, the only other restriction is that an Associative Array generally only contains unique keys. Multiple keys may refer to the same value, but the keys themselves should be unique.

If we want to add an item to the Hash Table, the first thing we need to do is find out what index in the Array the item should go into.

Now, let's say in this example the Hash Table is storing objects that represent people. So, let's try adding a person, Jane, to the Hash Table. What we'll do, is we'll take the object represented by Jane, and we will find the index that the value Jane should be stored at, using Jane's name as the key. So, we have a method called GetIndex that takes a string, and it returns an integer that is the index into the Array where Jane's object should be stored.

What we've seen here is that we're able to add these objects that are people objects into an Array based on their name as the key. So we don't know that Jane is stored at the third Array index, but we know we have a method, GetIndex, that when given the string Jane will return the #2, which is the third index in the zero-indexed Array, and allow us to store or retrieve Jane from that location by name.

It's a way for us to store Key/Value Pairs into an opaque Data Structure that we view as an Associative Array using a key other than the Array index to add and remove the item.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Hash_Table.png?raw=true)

### Hashing

Hashing is a process that derives a fixed size result from an arbitrary input. what it means by that is that any string of any length when Hashed would return a fixed size, or in this case, a 32-bit integer Hash value.

Fixed size simply means that every input returns a Hash code of the same size or type. The Hash codes we'll be looking at are 32-bit integer values, but some Hashing algorithms return smaller values, and some return much larger values.

Hashing algorithms have several properties. One is an invariant, and some are ideals.

- Stable - a stable algorithm returns the same output given the same input always. So, if you pass in the same string a million times, you should get that same Hash code value back a million times. It's an invariant; every Hashing algorithm has to be stable; if it's not stable it's not useful.
- Uniform - a uniform Hash is a Hash that is distributed evenly throughout the available range. So, if we're talking about 32-bit integers, that's really 4 billion values, give or take, that we're working with. So, if you pass in a million strings, you'd expect to see, you know, roughly a million values. Now, there are some mathematical rules that basically say, hey look, you're going to have Collisions, you're not going to have perfect uniformity, and it's impossible to have perfect uniformity, because there are certainly more than 4 billion potential strings in the universe. So once you've given more strings than there are potential integer values, you're going to have some inputs that produce the same outputs.
- Efficient - If the Hashing algorithm isn't efficient either in time or in space, those are things you might not want to use if you're talking about something that needs to be done billions of times in a process. You know, if it takes 3 milliseconds to do something, and you need to do it 3 billion times, that's a long time. If you can cut that down to 3 microseconds, you have saved multiple orders of magnitude off your processing time, and that's something that you need to look at when you're Hashing.
- Secure - For some problem domains, security is second only to stability, and what a secure Hash says is that given a Hash value, finding an input that could derive to that same value is infeasible. So, if I Hash a string and I get a result back, I don't want to be able to give that Hash code to someone and have them be able to figure out another string or perhaps the original string that led to that Hash value.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Hash_Table_2.png?raw=true)

### Hashing a String

String Hashing algorithms.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Hash_Table_3.png?raw=true)

We can do that with ASCII, because each ASCII character is 8 bits, one byte. An integer is 32 bits, or 4 bytes, so we can take four ASCII characters, cram them together and say hey, it's an integer.

You take the bytes of those characters and then just cram them together into a 32-bit value. So now let's look at the next four bytes, and they have their own value. Let's add our current Hash value with the new value; now it wraps back around, because we're doing 32-bit math. So, when you add two really big numbers in a 32-bit space they wrap around to a negative value.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Hash_Table_4.png?raw=true)

The. NET framework ships with several, they're good, these are things thought out by mathematicians, by really smart guys over many years that are vetted out by the community, hackers have attacked them for years, a lot of the problems have been found, and solutions are being derived from those. Don't create your own Hashing algorithm.

Pick the right Hash for the job at hand. Look at the characteristics of the problem space that you're working in, and pick the Hashing algorithm that applies best to it.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Hash_Table_5.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Hash_Table_Hashing_Code.png?raw=true)

unchecked, and the reason is that in C# when you're doing integer math, if you overflow the integer, it's going to throw an exception, unless you have it in an unchecked block, in which case it's going to wrap around from positive to negative. So, it's just something that we need to do in order to have the integer math work the way we expect.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Hash_Table_Hashing_Code_2.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Hash_Table_Hashing_Code_3.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Hash_Table_Hashing_Code_4.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Hash_Table_Hashing_Code_5.png?raw=true)

### Adding Data

we're going to get the hashCode for Jane, and that hashCode is some integer. Now remember, that integer is going to be anything in the integer value space; certainly it's unlikely that it's going to be somewhere between 0 and 8, so this is not our index into the Array, this is the hashCode for the string Jane. The index into the Array we're going to do a modulus of the hashCode with the Array length, and what that's going to do is take that kind of random but stable hashCode, and give us a value from 0 to 8, so we can use that hashCode to figure out the index into the Array. Once we have the index we can assign Jane to it, and now Jane is in the Hash Table.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Hash_Table_Adding.png?raw=true)

### Handling Collisions

Collisions are inevitable. Collision is when two distinct items have the same Hash value. So what ends up happening then is those items will be assigned to the same index in the Hash Table, and that's a problem. The end user, it shouldn't know that you had a Collision, it should be a totally transparent thing.

We didn't put Steve where the hashCode said Steve should be. So, next time we come looking for Steve, we need to take that into account.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Hash_Table_Open_Addressing.png?raw=true)

We don't know if there's a conflict. The Linked List could be empty, it could have 10 things in it, it could have 100 things in it, we don't know. Chaining allows us to not think about that. And what's nice about Chaining, as opposed to Open Addressing, is that later on when we come looking for Steve, all we have to do is find the index and now find out is Steve in the Linked List?

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Hash_Table_Chaining.png?raw=true)

### Growing the Table

The frequency of collisions is going to be a function of two separate issues. The first is how many available slots there are in the Array. If there are only nine items in the array like we've seen our samples so far, you have a more than 10% chance of having a collision on any given insert. And the other side of it is how populated the Array is. And this is known as the Load Factor; it's the ratio of filled slots to empty slots.

So, your Load Factor can determine of your nine available slots how many are free. If there's only one free, you have a less than 10% chance of not colliding. So, once you've hit a certain Load Factor or certain ratio of filled slots, it might be time to grow your Hash Table. You know, to say, alright, we had nine slots, only two were open, let's double the size. Now we'll have 18 slots and we'll have 11 open, and we've cut our chance of having a collision down dramatically. So, the way this works is when we're adding an item. The first thing we have to figure out is, is our current fillFactor or our current Load Factor greater than or equal to whatever maximum we decided. If we have a Hash Table backed by an Array with 100 items, 75 of which are full, our fillFactor is 75%. If our maxFillFactor is 75%, or 70%, or anything lower than 75%, we need to grow the Array.

All we do is we allocate a newArray, and that Array is going to be twice as long as the current Array that we have, and we enumerate over each item in the existing Array, we add the item to the new Array. It's actually pretty straightforward when you think about it, because when you add that item to the new Array it's going to be Hashed into that Array, but this time it's going to take into account that there are twice as many buckets available, so it's very likely that it'll be Hashed to a different location.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Hash_Table_Growing.png?raw=true)

### Removing Data

When we implemented the Add algorithm, we had to decide, were we going to Chain Collisions together, or were we going to use Open Addressing to handle Collisions? And that decision impacts the rest of the Hash Table.

We're going to do is remove the item by key. So we stored an object with the key Jane, which represents the value that is the person object for the user Jane.

With Open Addressing, this is the case where, in the case of the Collision, we move the object forward, what we say is let's get the index of where we think that object should be. So, we think Jane should be at index 3. If the value at index 3 is non-null, check if it's Jane. If it is Jane, remove it, but if it's not Jane, let's assume a collision might have occurred, and go check the next index. And now we kind of repeat here. If that next index is non-null, if it's Jane, remove it; if it's not Jane, continue. And we keep on doing this until we either find a null entry or we find Jane. But this does get more complex. If you had multiple collisions that all caused the walk forward, when you remove an item you create a null slot, and now what you have to do is look at every non-null item after that item, and figure out if it was a result of a collision, and if it was you have to re-add it back into the table. And this gets really kind of tedious to maintain.

Chaining, and this is because it's a lot easier to conceptually understand, and the point of this is not to understand all the subtleties of Open Addressing, the point of this is to understand Hash Tables, so let's not focus on Open Addressing, let's focus on Chaining. All you do with Chaining is you get the index for the key that you're looking at; so we get the index for Jane, index 2. And then, we look at index 2. Is there a Linked List in that index? If there is, then look and see if Jane's in it, and if she is, take her out, and if there's not a Linked List there, then Jane's not there, so there's nothing to remove. It's much simpler.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Hash_Table_Removing.png?raw=true)

### Finding Data

The Find algorithm in a Hash Table works a lot like the Remove algorithm, and what I mean by that is it depends entirely on what Collision detection algorithm you've chosen, and it has to handle them appropriately.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Hash_Table_Finding.png?raw=true)

### Enumerating

As with all the other collection types we've looked at, the ability to Enumerate the Keys and Values in the Hash Table is important, and whether you're enumerating a Linked List, or the Queue or Stack, or even the Binary Tree, you need to be able to do it, and a Hash Table is no different. 

The difference is, with a Hash Table you need to generally have two different enumeration styles, one to enumerate all the keys, and one to enumerate all the values. Just like Remove and Find, the type of Collision detection we're doing here is everything.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Hash_Table_Enumerating.png?raw=true)

So, that's where a Hash Table really shines. Key/Value pairs where we're updating the values, and we have a stable key.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Hash_Table_Summary.png?raw=true)
