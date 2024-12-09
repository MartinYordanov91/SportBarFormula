using Microsoft.AspNetCore.Identity;
using Moq;
using System.Linq.Expressions;

namespace SportBarFormula.UnitTests.Moq;

public static class MockUserManager
{
    public static Mock<UserManager<IdentityUser>> Create()
    {
        var store = new Mock<IUserStore<IdentityUser>>();
        var mockUserManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);

        // Seed users
        var users = new List<IdentityUser>
        {
            new IdentityUser { Id = "1", UserName = "user1", Email = "user1@example.com" },
            new IdentityUser { Id = "2", UserName = "user2", Email = "user2@example.com" },
            new IdentityUser { Id = "3", UserName = "user3", Email = "user3@example.com" },
            new IdentityUser { Id = "4", UserName = "user4", Email = "user4@example.com" },
            new IdentityUser { Id = "5", UserName = "user5", Email = "user5@example.com" }
        }.AsQueryable();

        // Mock the Users property to return the seeded users as IQueryable
        var asyncUsers = new TestAsyncEnumerable<IdentityUser>(users);
        mockUserManager.Setup(u => u.Users).Returns(asyncUsers);

        // Mock the FindByIdAsync method to return a user based on ID
        mockUserManager.Setup(u => u.FindByIdAsync(It.IsAny<string>()))
                       .ReturnsAsync((string id) => users.FirstOrDefault(u => u.Id == id));

        return mockUserManager;
    }

    // Helper class for async IQueryable
    private class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public TestAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable) { }
        public TestAsyncEnumerable(Expression expression) : base(expression) { }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        private class TestAsyncEnumerator<TItem> : IAsyncEnumerator<TItem>
        {
            private readonly IEnumerator<TItem> _enumerator;
            public TestAsyncEnumerator(IEnumerator<TItem> enumerator) => _enumerator = enumerator;
            public ValueTask DisposeAsync()
            {
                _enumerator.Dispose();
                return ValueTask.CompletedTask;
            }

            public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(_enumerator.MoveNext());
            public TItem Current => _enumerator.Current;
        }
    }
}
