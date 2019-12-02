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

