using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueue_Tests
{
    [TestMethod]
    public void EnqueueDequeue_HighestPriorityRemovedFirst()
    {
        // Test to check if the highest priority is removed first.
        var queue = new PriorityQueue();
        queue.Enqueue("Item1", 1);
        queue.Enqueue("Item2", 2);
        queue.Enqueue("Item3", 3);

        // Dequeue should return "Item3" first, as it has the highest priority.
        var result = queue.Dequeue();
        Assert.AreEqual("Item3", result);

        // After "Item3" is removed, the next item should be "Item2" (second highest priority).
        result = queue.Dequeue();
        Assert.AreEqual("Item2", result);

        // Finally, "Item1" should be dequeued last.
        result = queue.Dequeue();
        Assert.AreEqual("Item1", result);
    }

    [TestMethod]
    public void EnqueueDequeue_SamePriorityFIFO()
    {
        // Test to ensure FIFO order for items with the same priority.
        var queue = new PriorityQueue();
        queue.Enqueue("Item1", 1);
        queue.Enqueue("Item2", 1); // Same priority as Item1
        queue.Enqueue("Item3", 1); // Same priority as Item1 and Item2

        // Dequeue should return "Item1" first (FIFO order)
        var result = queue.Dequeue();
        Assert.AreEqual("Item1", result);

        // Then "Item2" should be dequeued
        result = queue.Dequeue();
        Assert.AreEqual("Item2", result);

        // Finally, "Item3" should be dequeued
        result = queue.Dequeue();
        Assert.AreEqual("Item3", result);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Dequeue_EmptyQueue_ThrowsException()
    {
        // Test to ensure an exception is thrown when trying to dequeue from an empty queue.
        var queue = new PriorityQueue();
        queue.Dequeue();  // Should throw an InvalidOperationException.
    }

    [TestMethod]
    public void EnqueueDequeue_SingleItem()
    {
        // Test to ensure a single item can be enqueued and dequeued correctly.
        var queue = new PriorityQueue();
        queue.Enqueue("Item1", 1);

        // Dequeue should return "Item1".
        var result = queue.Dequeue();
        Assert.AreEqual("Item1", result);
    }
}
