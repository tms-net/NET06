---
marp: true
theme: uncover
---

![bg](img/title_page_bg.jpg)

# Entity Framework Core Saving, Testing

---
<!-- _class: slide -->
## Basic Operations
Each context instance has a ChangeTracker that is responsible for keeping track of changes that need to be written to the database. As you make changes to instances of your entity classes, these changes are recorded in the ChangeTracker and then written to the database when you call SaveChanges. The database provider is responsible for translating the changes into database-specific operations (for example, INSERT, UPDATE, and DELETE commands for a relational database).

### Adding Data
Use the DbSet.Add method to add new instances of your entity classes. The data will be inserted in the database when you call SaveChanges.
```
using (var context = new BloggingContext())
{
    var blog = new Blog { Url = "http://example.com" };
    context.Blogs.Add(blog);
    context.SaveChanges();
}
```

### Updating Data
EF will automatically detect changes made to an existing entity that is tracked by the context. This includes entities that you load/query from the database, and entities that were previously added and saved to the database. Simply modify the values assigned to properties and then call SaveChanges.
```
using (var context = new BloggingContext())
{
    var blog = context.Blogs.First();
    blog.Url = "http://example.com/blog";
    context.SaveChanges();
}
```

### Deleting Data
Use the DbSet.Remove method to delete instances of your entity classes.

If the entity already exists in the database, it will be deleted during SaveChanges. If the entity has not yet been saved to the database (that is, it is tracked as added) then it will be removed from the context and will no longer be inserted when SaveChanges is called.
```
using (var context = new BloggingContext())
{
    var blog = context.Blogs.First();
    context.Blogs.Remove(blog);
    context.SaveChanges();
}
```

### Multiple Operations in a single SaveChanges
You can combine multiple Add/Update/Remove operations into a single call to SaveChanges.
*For most database providers, SaveChanges is transactional. This means all the operations will either succeed or fail and the operations will never be left partially applied.*
```
using (var context = new BloggingContext())
{
    // seeding database
    context.Blogs.Add(new Blog { Url = "http://example.com/blog" });
    context.Blogs.Add(new Blog { Url = "http://example.com/another_blog" });
    context.SaveChanges();
}

using (var context = new BloggingContext())
{
    // add
    context.Blogs.Add(new Blog { Url = "http://example.com/blog_one" });
    context.Blogs.Add(new Blog { Url = "http://example.com/blog_two" });

    // update
    var firstBlog = context.Blogs.First();
    firstBlog.Url = "";

    // remove
    var lastBlog = context.Blogs.OrderBy(e => e.BlogId).Last();
    context.Blogs.Remove(lastBlog);

    context.SaveChanges();
}
```

## Related Data
In addition to isolated entities, you can also make use of the relationships defined in your model.

### Adding related entities
If you create several new related entities, adding one of them to the context will cause the others to be added too.
```
using (var context = new BloggingContext())
{
    var blog = new Blog
    {
        Url = "http://blogs.msdn.com/dotnet",
        Posts = new List<Post>
        {
            new Post { Title = "Intro to C#" },
            new Post { Title = "Intro to VB.NET" },
            new Post { Title = "Intro to F#" }
        }
    };

    context.Blogs.Add(blog);
    context.SaveChanges();
}
```

If you reference a new entity from the navigation property of an entity that is already tracked by the context, the entity will be discovered and inserted into the database.
```
using (var context = new BloggingContext())
{
    var blog = context.Blogs.Include(b => b.Posts).First();
    var post = new Post { Title = "Intro to EF Core" };

    blog.Posts.Add(post);
    context.SaveChanges();
}
```

If you change the navigation property of an entity, the corresponding changes will be made to the foreign key column in the database.
```
using (var context = new BloggingContext())
{
    var blog = new Blog { Url = "http://blogs.msdn.com/visualstudio" };
    var post = context.Posts.First();

    post.Blog = blog;
    context.SaveChanges();
}
```

### Removing relationships
You can remove a relationship by setting a reference navigation to null, or removing the related entity from a collection navigation.

*Removing a relationship can have side effects on the dependent entity, according to the cascade delete behavior configured in the relationship.*

By default, for required relationships, a cascade delete behavior is configured and the child/dependent entity will be deleted from the database. For optional relationships, cascade delete is not configured by default, but the foreign key property will be set to null.
```
using (var context = new TMSContext())
{
    var student = context.Students.Include(s => s.Homeworks).First();
    var homework = student.Homeworks.First();

    student.Homeworks.Remove(post);
    context.SaveChanges();
}
```
```
using (var context = new TMSContext())
{
    var student = context.Students.Include(s => s.Homeworks).First();

    context.Remove(student);
    context.SaveChanges();
}
```
```
using (var context = new TMSContext())
{
    var homework = student.Homeworks.First();
    homework.Student = null;
    context.SaveChanges();
}
```

**TODO: View SQL generated during**

*Deleting entities that are no longer associated with any principal/dependent is known as "deleting orphans".*

### Where cascading behaviors happen
Cascading behaviors can be applied to:
 - Entities tracked by the current DbContext
 - Entities in the database that have not been loaded into the context

**Cascade delete of tracked entities**
EF Core always applies configured cascading behaviors to tracked entities. This means that if the application loads all relevant dependent/child entities into the DbContext, as is shown in the examples above, then cascading behaviors will be correctly applied regardless of how the database is configured.

**Cascade delete in the database**
Many database systems also offer cascading behaviors that are triggered when an entity is deleted in the database.
```
ALTER TABLE [dbo].[homeworks]  WITH CHECK ADD  CONSTRAINT [FK_homeworks_Students_NewStudentId] FOREIGN KEY([NewStudentId])
REFERENCES [dbo].[Students] ([StudentId]) ON DELETE CASCADE
GO
```

If we know that the database is configured like this, then we can delete a parent without first loading related entities and the database will take care of deleting all the entities that were related to that principal.
```
using (var context = new TMSContext())
{
    var student = context.Students.First();

    context.Remove(student);
    context.SaveChanges();
}
```

**Impact on SaveChanges behavior**
To review the behavior of principal/parent and related entities according to EF configuration see [https://docs.microsoft.com/en-us/ef/core/saving/cascade-delete#impact-on-savechanges-behavior](https://docs.microsoft.com/en-us/ef/core/saving/cascade-delete#impact-on-savechanges-behavior)

### Handling Concurrency Conflicts
See [Concurrency Tokens](https://docs.microsoft.com/en-us/ef/core/modeling/concurrency?tabs=data-annotations) for details on how to configure concurrency tokens in your model.

Database concurrency refers to situations in which multiple processes or users access or change the same data in a database at the same time. Concurrency control refers to specific mechanisms used to ensure data consistency in presence of concurrent changes.

EF Core implements **optimistic concurrency control**, meaning that it will let multiple processes or users make changes independently without the overhead of synchronization or locking.

### How concurrency control works in EF Core
Properties configured as concurrency tokens are used to implement optimistic concurrency control: whenever an update or delete operation is performed during SaveChanges, the value of the concurrency token on the database is compared against the original value read by EF Core.

 - If the values match, the operation can complete.
 - If the values do not match, EF Core assumes that another user has performed a conflicting operation and aborts the current transaction.

The situation when another user has performed an operation that conflicts with the current operation is known as *concurrency conflict*.

On relational databases EF Core includes a check for the value of the concurrency token in the `WHERE` clause of any `UPDATE` or `DELETE` statements. After executing the statements, EF Core reads the number of rows that were affected.

If no rows are affected, a concurrency conflict is detected, and EF Core throws `DbUpdateConcurrencyException`.

For example, we may want to configure `LastName` on `Student` to be a concurrency token. Then any update operation on Person will include the concurrency check in the `WHERE` clause:
```
UPDATE [Students] SET [Name] = @p1
WHERE [StudentId] = @p0 AND [LastName] = @p2;
```

### Resolving concurrency conflicts
Resolving a concurrency conflict involves merging the pending changes from the current DbContext with the values in the database. What values get merged will vary based on the application and may be directed by user input.

There are three sets of values available to help resolve a concurrency conflict:

 - **Current** values are the values that the application was attempting to write to the database.
 - **Original** values are the values that were originally retrieved from the database, before any edits were made.
 - **Database** values are the values currently stored in the database.
 
 The general approach to handle a concurrency conflicts is:

 - Catch DbUpdateConcurrencyException during SaveChanges.
 - Use DbUpdateConcurrencyException.Entries to prepare a new set of changes for the affected entities.
 - Refresh the original values of the concurrency token to reflect the current values in the database.
 - Retry the process until no conflicts occur.

In the following example, Person.FirstName and Person.LastName are set up as concurrency tokens.
```
using var context = new PersonContext();
// Fetch a person from database and change phone number
var person = context.People.Single(p => p.PersonId == 1);
person.PhoneNumber = "555-555-5555";

// Change the person's name in the database to simulate a concurrency conflict
context.Database.ExecuteSqlRaw(
    "UPDATE dbo.People SET FirstName = 'Jane' WHERE PersonId = 1");

var saved = false;
while (!saved)
{
    try
    {
        // Attempt to save changes to the database
        context.SaveChanges();
        saved = true;
    }
    catch (DbUpdateConcurrencyException ex)
    {
        foreach (var entry in ex.Entries)
        {
            if (entry.Entity is Person)
            {
                var proposedValues = entry.CurrentValues;
                var databaseValues = entry.GetDatabaseValues();

                foreach (var property in proposedValues.Properties)
                {
                    var proposedValue = proposedValues[property];
                    var databaseValue = databaseValues[property];

                    // TODO: decide which value should be written to database
                    // proposedValues[property] = <value to be saved>;
                }

                // Refresh original values to bypass next concurrency check
                entry.OriginalValues.SetValues(databaseValues);
            }
            else
            {
                throw new NotSupportedException(
                    "Don't know how to handle concurrency conflicts for "
                    + entry.Metadata.Name);
            }
        }
    }
}
```