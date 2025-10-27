namespace AddTwoNumber;

public static class Program
{
    public static void Main()
    {
        // You are given two non-empty linked lists representing two non-negative integers.
        // The digits are stored in reverse order, and each of their nodes contains a single digit.
        // Add the two numbers and return the sum as a linked list.
        // You may assume the two numbers do not contain any leading zero, except the number 0 itself.

        ListNode l1 = new(2, new(4, new(3)));
        ListNode l2 = new(5, new(6, new(4)));

        Solution.AddTwoNumbers(l1, l2); 
    }
}

public static class Solution
{
    public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        ListNode fakeFirstNode = new(0);
        ListNode aggregatedLinkedList = fakeFirstNode;
        int overflow = 0;

        while (l1 != null || l2 != null || overflow != 0)
        {
            int x = l1?.val ?? 0;
            int y = l2?.val ?? 0;

            int sum = x + y + overflow;
            overflow = sum / 10;

            aggregatedLinkedList.next = new ListNode(sum % 10);
            aggregatedLinkedList = aggregatedLinkedList.next;

            l1 = l1?.next;
            l2 = l2?.next;
        }

        return fakeFirstNode.next!;
    }
}

public class ListNode
{
    public int val;
    public ListNode next;

    public ListNode(int val = 0, ListNode next = null)
    {
        this.val = val;
        this.next = next;
    }
}