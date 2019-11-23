# Project

Example of api (ASP.NET Core 3) with database (SQLite - Entity Framework Core 3).

## Program

```cs
public static class Program
{
    public static void Main()
    {
        Host.CreateDefaultBuilder().ConfigureWebHostDefaults(x => x.UseStartup<Startup>()).Build().Run();
    }
}
```

```cs
public class Startup
{
    public void Configure(IApplicationBuilder application, Context context)
    {
        application.UseCors(cors => cors.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        application.UseHsts();
        application.UseHttpsRedirection();
        application.UseRouting();
        application.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.Seed();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors();
        services.AddControllers();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddDbContextPool<Context>(options => options.UseSqlite("Data Source = Database.db"));
    }
}
```

## User

```cs
[ApiController]
[Route("[controller]")]
public sealed class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (id == default)
        {
            return BadRequest();
        }

        _userService.Delete(id);

        return NoContent();
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        if (id == default)
        {
            return BadRequest();
        }

        var result = _userService.Get(id);

        if (result == default)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    public IActionResult Get()
    {
        var result = _userService.List();

        if (result == default)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPatch("{id}/email")]
    public IActionResult PatchEmail(int id, UserModel model)
    {
        if (id == default || model == default)
        {
            return BadRequest();
        }

        _userService.UpdateEmail(model);

        return NoContent();
    }

    [HttpPost]
    public IActionResult Post(UserModel model)
    {
        if (model == default)
        {
            return BadRequest();
        }

        _userService.Add(model);

        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, UserModel model)
    {
        if (id == default || model == default)
        {
            return BadRequest();
        }

        model.Id = id;

        _userService.Update(model);

        return NoContent();
    }
}
```

```cs
public interface IUserService
{
    void Add(UserModel model);

    void Delete(int id);

    UserModel Get(int id);

    IEnumerable<UserModel> List();

    void Update(UserModel model);

    void UpdateEmail(UserModel model);
}
```

```cs
public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void Add(UserModel model)
    {
        var entity = UserFactory.Create(model);

        _userRepository.Add(entity);

        model.Id = entity.Id;
    }

    public void Delete(int id)
    {
        _userRepository.Delete(id);
    }

    public UserModel Get(int id)
    {
        var entity = _userRepository.Get(id);

        return UserFactory.Create(entity);
    }

    public IEnumerable<UserModel> List()
    {
        return _userRepository.List().Select(UserFactory.Create);
    }

    public void Update(UserModel model)
    {
        var entity = UserFactory.Create(model);

        _userRepository.Update(entity);
    }

    public void UpdateEmail(UserModel model)
    {
        _userRepository.Update(model.Id, new { model.Email });
    }
}
```

```cs
public interface IUserRepository : IRepository<UserEntity> { }
```

```cs
public sealed class UserRepository : Repository<UserEntity>, IUserRepository
{
    public UserRepository(Context context) : base(context) { }
}
```

```cs
public sealed class UserModel
{
    public UserModel() { }

    public UserModel
    (
        int id,
        string name,
        string surname,
        string email
    )
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }
}
```

```cs
public sealed class UserEntity
{
    public UserEntity
    (
        int id,
        string name,
        string surname,
        string email
    )
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public string Surname { get; private set; }

    public string Email { get; private set; }

    public void UpdateName(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }

    public void UpdateEmail(string email)
    {
        Email = email;
    }
}
```

```cs
public static class UserFactory
{
    public static UserEntity Create(UserModel model)
    {
        if (model == default) return default;

        return new UserEntity(model.Id, model.Name, model.Surname, model.Email);
    }

    public static UserModel Create(UserEntity entity)
    {
        if (entity == default) return default;

        return new UserModel(entity.Id, entity.Name, entity.Surname, entity.Email);
    }
}
```

```cs
public sealed class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable(nameof(UserEntity));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
        builder.Property(x => x.Surname).IsRequired().HasMaxLength(250);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(500);
    }
}
```

## Database

```cs
public sealed class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
    }
}
```

```cs
public static class ContextExtensions
{
    public static void Seed(this Context context)
    {
        context.Add(new UserEntity(default, "Ada", "Lovelace", "ada.lovelace@email.com"));
        context.Add(new UserEntity(default, "Albert", "Einstein", "albert.einstein@email.com"));
        context.Add(new UserEntity(default, "Galileu", "Galilei", "galileu.galilei@email.com"));
        context.Add(new UserEntity(default, "Isaac", "Newton", "isaac.newton@email.com"));
        context.Add(new UserEntity(default, "Marie", "Curie", "marie.curie@email.com"));
        context.Add(new UserEntity(default, "Nikola", "Tesla", "nikola.tesla@email.com"));
        context.Add(new UserEntity(default, "Stephen", "Hawking", "stephen.hawking@email.com"));

        context.SaveChanges();
    }
}
```

```cs
public interface IRepository<T>
{
    void Add(T item);

    void Delete(int id);

    T Get(int id);

    IEnumerable<T> List();

    void Update(T item);

    void Update(int id, object item);
}
```

```cs
public abstract class Repository<T> : IRepository<T> where T : class
{
    private readonly Context _context;

    protected Repository(Context context)
    {
        _context = context;
    }

    public void Add(T item)
    {
        _context.Set<T>().Add(item);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        _context.Set<T>().Remove(Get(id));
        _context.SaveChanges();
    }

    public T Get(int id)
    {
        return _context.Set<T>().Find(id);
    }

    public IEnumerable<T> List()
    {
        return _context.Set<T>().ToList();
    }

    public void Update(T item)
    {
        _context.Set<T>().Update(item);
        _context.SaveChanges();
    }

    public void Update(int id, object item)
    {
        _context.Entry(Get(id)).CurrentValues.SetValues(item);
        _context.SaveChanges();
    }
}
```
