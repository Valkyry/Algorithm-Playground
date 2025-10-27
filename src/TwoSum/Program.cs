using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace TwoSum;

public static class Program
{
    public static void Main()
    {
        // Given an array of integers nums and an integer target, return indices of the two numbers such that they add up to target.
        // You may assume that each input would have exactly one solution, and you may not use the same element twice.
        // You can return the answer in any order.
        
        BenchmarkRunner.Run<Solution>();
        
        //here is the result if u dont want to run it :) 
        // | Method           | Mean     | Error     | StdDev    | Ratio | Gen0   | Gen1   | Allocated | Alloc Ratio |
        // |----------------- |---------:|----------:|----------:|------:|-------:|-------:|----------:|------------:|
        // | TwoSumBruteForce | 7.246 us | 0.0126 us | 0.0105 us |  1.00 |      - |      - |      32 B |        1.00 |
        // | TwoSumDictionary | 3.722 us | 0.0424 us | 0.0354 us |  0.51 | 1.9226 | 0.0763 |   16096 B |      503.00 |
        //
        // TwoSumDictionary is twice faster but allocates 503 times more memory :)
    }
}

[MemoryDiagnoser]
public class Solution
{
    private int[] _nums = null!;
    private int _target;

    [GlobalSetup]
    public void Setup()
    {
        _nums = new int[10_000];
        var random = new Random(42);
        for (int i = 0; i < _nums.Length; i++)
            _nums[i] = random.Next(1, 10_000);

        _target = _nums[123] + _nums[789];
    }
    
    //Brute Force (O(n^2))
    [Benchmark(Baseline = true)]
    public int[] TwoSumBruteForce()
    {
        for (int i = 0; i < _nums.Length; i++)
        {
            for (int j = i + 1; j < _nums.Length; j++)
            {
                if (_nums[i] + _nums[j] == _target)
                    return [i, j];
            }
        }

        throw new InvalidOperationException("Nothing found!");
    }
    
    //complexity (O(n))
    [Benchmark]
    public int[] TwoSumDictionary()
    {
        Dictionary<int, int> dictionary = new();

        for (int i = 0; i < _nums.Length; i++)
        {
            int complement = _target - _nums[i];
            
            if (dictionary.TryGetValue(complement, out var value))
            {
                return [value, i];
            }

            dictionary[_nums[i]] = i;
        }
        
        throw new InvalidOperationException("Nothing found!");
    }
}