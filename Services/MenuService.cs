using CampusEats.Api.Dtos;
using CampusEats.Api.Models;

namespace CampusEats.Api.Services;

public class MenuService : IMenuService
{
    private readonly List<MenuItem> _items;
    private int _nextId;

    public MenuService()
    {
        // Seed in-memory data
        _items = new()
        {
            new()
            {
                Id = 1,
                Name = "Kottu Roti",
                Price = 750m,
                Category = "Mains",
                Available = true
            },
            new()
            {
                Id = 2,
                Name = "Fried Rice",
                Price = 850m,
                Category = "Mains",
                Available = true
            },
            new()
            {
                Id = 3,
                Name = "Watalappan",
                Price = 350m,
                Category = "Dessert",
                Available = true
            }
        };

        _nextId = 4;
    }

    // Convert internal model to public DTO
    private static MenuItemDto ToDto(MenuItem m) =>
        new(
            m.Id,
            m.Name,
            m.Price,
            m.Category,
            m.Available
        );

    // GET: Return all menu items
    public IEnumerable<MenuItemDto> GetAll() =>
        _items.Select(ToDto);

    // GET: Return one menu item by ID
    public MenuItemDto? GetById(int id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);

        return item is null ? null : ToDto(item);
    }

    // POST: Create a new menu item
    public MenuItemDto Create(CreateMenuItemDto dto)
    {
        var item = new MenuItem
        {
            Id = _nextId++,
            Name = dto.Name,
            Price = dto.Price,
            Category = dto.Category,
            Available = true
        };

        _items.Add(item);

        return ToDto(item);
    }

    // PUT: Update an existing menu item
    public bool Update(int id, CreateMenuItemDto dto)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);

        if (item is null)
            return false;

        item.Name = dto.Name;
        item.Price = dto.Price;
        item.Category = dto.Category;

        return true;
    }

    // DELETE: Delete a menu item
    public bool Delete(int id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);

        if (item is null)
            return false;

        _items.Remove(item);

        return true;
    }
}