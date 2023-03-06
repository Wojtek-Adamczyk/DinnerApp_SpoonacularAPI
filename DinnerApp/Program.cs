using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DinnerApp;
using Newtonsoft.Json;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Enter the ingredients separated by commas");
        string ingredients = Console.ReadLine();
        Console.WriteLine();

        var recipes = await GetRecipes(ingredients);

        if (recipes != null && recipes.Count > 0)
        {
            Console.WriteLine($"You can make at least {recipes.Count} dishes\n");

            foreach (var recipe in recipes) { Console.WriteLine(recipe.title); }
        }
        else Console.WriteLine("No dishes found. Go to Lidl now");

    }

    static async Task<List<Recipe>> GetRecipes(string ingredients)
    {
        string url = $"https://api.spoonacular.com/recipes/findByIngredients?ingredients={ingredients}&number=10&apiKey=2058adc96bb846428c3703dab9ba0d2c";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<List<Recipe>>(content);

        return result;
    }
}