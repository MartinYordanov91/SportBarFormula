# SportBarFormula Project

## Overview
The **SportBarFormula** project is a full-featured web application designed to manage operations in a sports bar, such as managing users, roles, categories, menu items, orders, and reservations. The application uses a modular architecture, combining **ASP.NET Core** for the backend, **Entity Framework Core** for database management, and **ASP.NET Core Identity** for authentication and authorization.

This documentation is structured to provide a detailed description of the application's components, including controllers, services, repositories, extensions, and unit tests. Each module is documented based on the summary comments provided throughout the codebase.

---

## Table of Contents

1. [Modules](#modules)
2. [Controllers](#controllers)
   - [Admin Controllers](#admin-controllers)
   - [Application Controllers](#application-controllers)
   - [Identity Controllers](#identity-controllers)
3. [Services](#services)
4. [Repositories](#repositories)
5. [Extensions](#extensions)
6. [Testing](#testing)
7. [Setup](#setup)
8. [License](#license)

---

## Modules

### SportBarFormula.Core
This module contains the core business logic for managing categories, menu items, orders, reservations, and users. Services in this module handle validation, transformations, and interactions with the repositories.

### SportBarFormula.Infrastructure
This module is responsible for data persistence and migrations. It contains the Entity Framework Core DbContext and repositories for managing entities such as categories, menu items, orders, and reservations.

### SportBarFormula.Areas
Organized areas for admin-specific and user-specific functionality:
- **Admin**: Controllers and views for managing the application's administrative tasks.
- **Identity**: Controllers and pages for handling user authentication and registration using ASP.NET Core Identity.

### SportBarFormula.UnitTests
This module contains unit tests to validate the functionality of services, controllers, and repositories. It uses an in-memory database for testing data access.

---

## Controllers

### Admin Controllers

#### HomeController
- **Purpose**: Manages the admin dashboard and provides an overview of administrative tasks.
- **Actions**:
  - `Index()`: Displays the home page for administrators.
    - **Returns**: A view for the admin home page.

#### RoleController
- **Purpose**: Manages roles in the application.
- **Constructor Parameters**:
  - `RoleManager<IdentityRole>`: Instance to handle role operations.
- **Actions**:
  - `Index()`: Lists all roles in the application.
    - **Returns**: A view containing a list of roles.

#### UsersController
- **Purpose**: Manages user accounts and their associated roles.
- **Constructor Parameters**:
  - `IUserService`: Service for managing users and their roles.
- **Actions**:
  - `Index()`: Displays a list of all users.
    - **Returns**: A view containing a list of users.
  - `ManageRoles(string userId)`: Displays the manage roles view for a specific user.
    - **Parameters**: `userId` - The ID of the user.
    - **Returns**: A view for managing the user's roles.
  - `ManageRoles(string userId, List<string> roles)`: Updates the roles for a user.
    - **Parameters**:
      - `userId`: The ID of the user.
      - `roles`: List of roles to assign to the user.
    - **Returns**: A redirect to the Index action.

---

### Application Controllers

#### CategoryController
- **Purpose**: Manages categories for menu items.
- **Constructor Parameters**:
  - `ICategoryService`: Service for managing categories.
  - `IModelStateLoggerService`: Logger service for capturing model state errors.
- **Actions**:
  - `Index()`: Retrieves and displays all categories.
    - **Returns**: A view containing all categories.
  - `Create()`: Displays a form to create a new category.
    - **Returns**: A view for creating a new category.
  - `Create(CategoryViewModel model)`: Processes the creation of a new category.
    - **Parameters**:
      - `model`: The view model containing category details.
    - **Returns**: Redirects to Index on success or displays the form with errors on failure.
  - `Edit(int id)`: Displays a form to edit an existing category.
    - **Parameters**:
      - `id`: The ID of the category to edit.
    - **Returns**: A view for editing the category or NotFound if it doesn’t exist.
  - `Edit(CategoryViewModel model)`: Updates an existing category.
    - **Parameters**:
      - `model`: The view model containing updated category details.
    - **Returns**: Redirects to Index on success or displays the form with errors on failure.
  - `Delete(int id)`: Deletes a category by its ID.
    - **Parameters**:
      - `id`: The ID of the category to delete.
    - **Returns**: Redirects to Index on success or NotFound if the category doesn’t exist.

#### MenuController
- **Purpose**: Manages menu items, including their details and categories.
- **Constructor Parameters**:
  - `IMenuItemService`: Service for managing menu items.
  - `ICategoryService`: Service for managing categories.
  - `IModelStateLoggerService`: Logger service for capturing model state errors.
- **Actions**:
  - `Index(AllMenuItemsQueryModel queryModel)`: Displays all menu items with filtering and pagination.
    - **Parameters**:
      - `queryModel`: Query model for filtering, searching, and sorting menu items.
    - **Returns**: A view containing filtered and paginated menu items.
  - `Details(int id)`: Shows details about a specific menu item.
    - **Parameters**:
      - `id`: The ID of the menu item.
    - **Returns**: A view containing menu item details.
  - `Create()`: Displays a form to create a new menu item.
    - **Returns**: A view for creating a menu item.
  - `Create(CreateMenuItemViewModel model)`: Processes the creation of a new menu item.
    - **Parameters**:
      - `model`: The view model containing menu item details.
    - **Returns**: Redirects to Index on success or displays the form with errors on failure.
  - `Edit(int id)`: Displays a form to edit a menu item.
    - **Parameters**:
      - `id`: The ID of the menu item to edit.
    - **Returns**: A view for editing the menu item or NotFound if it doesn’t exist.
  - `Edit(MenuItemEditViewModel model)`: Updates an existing menu item.
    - **Parameters**:
      - `model`: The view model containing updated menu item details.
    - **Returns**: Redirects to Index on success or displays the form with errors on failure.
  - `UnDelete(int id)`: Restores a previously deleted menu item.
    - **Parameters**:
      - `id`: The ID of the menu item to restore.
    - **Returns**: Redirects to the menu item’s details page.

#### OrderController
- **Purpose**: Manages orders and cart functionality.
- **Constructor Parameters**:
  - `UserManager<IdentityUser>`: User manager for handling user-specific operations.
  - `IOrderService`: Service for managing orders.
- **Actions**:
  - `AddToCart(int menuItemId, int quantity)`: Adds a menu item to the cart.
    - **Parameters**:
      - `menuItemId`: The ID of the menu item.
      - `quantity`: Quantity to add (default is 1).
    - **Returns**: Redirects to the cart page.
  - `MyCart()`: Displays the user’s current cart.
    - **Returns**: A view containing cart details.
  - `Checkout()`: Completes the checkout process for the current cart.
    - **Returns**: A redirect to the order confirmation page.

#### ReservationController
- **Purpose**: Handles table reservations for customers and administrators.
- **Constructor Parameters**:
  - `IModelStateLoggerService`: Logger service for model state errors.
  - `IReservationService`: Service for managing reservations.
  - `ITableService`: Service for managing table entities.
- **Actions**:
  - `Index()`: Lists all reservations for admins.
    - **Returns**: A view containing reservations.
  - `MyReservations()`: Shows the user’s current and past reservations.
    - **Returns**: A view with the user’s reservations.
  - `Create()`: Displays a form to create a new reservation.
    - **Returns**: A view for creating a reservation.
  - `Create(ReservationViewModel model)`: Processes the creation of a reservation.
    - **Parameters**:
      - `model`: The view model containing reservation details.
    - **Returns**: Redirects to the user’s reservation list on success.
  - `Edit(int id)`: Displays a form to edit an existing reservation.
    - **Parameters**:
      - `id`: The ID of the reservation to edit.
    - **Returns**: A view for editing the reservation or NotFound if it doesn’t exist.
  - `Edit(ReservationViewModel model)`: Processes the update of a reservation.
    - **Parameters**:
      - `model`: The view model containing updated reservation details.
    - **Returns**: Redirects to the reservation list on success or displays the form with errors on failure.
  - `Cancel(int id)`: Cancels a reservation.
    - **Parameters**:
      - `id`: The ID of the reservation to cancel.
    - **Returns**: Redirects to the reservation list.

### Identity Controllers
- These controllers are part of the ASP.NET Core Identity infrastructure and handle user authentication, login, and registration.

---

## Services

### Category Service
- **Purpose**: Provides business logic for managing categories.
- **Methods**:
  - `GetAllCategoriesAsync()`: Retrieves all categories.
    - **Returns**: A collection of `CategoryViewModel` containing category details.
  - `AddCategoryAsync(CategoryViewModel model)`: Adds a new category.
    - **Parameters**:
      - `model`: The view model containing category details.
    - **Returns**: A task representing the asynchronous operation.
  - `GetCategoryByIdAsync(int id)`: Retrieves a category by its ID.
    - **Parameters**:
      - `id`: The ID of the category.
    - **Returns**: The `CategoryViewModel` containing category details.
    - **Exceptions**:
      - `InvalidOperationException`: Thrown when no categories are found.
  - `UpdateCategoryAsync(CategoryViewModel model)`: Updates an existing category.
    - **Parameters**:
      - `model`: The view model containing updated category details.
    - **Returns**: A task representing the asynchronous operation.
  - `DeleteCategoryAsync(int id)`: Deletes a category by its ID if it has no associated menu items.
    - **Parameters**:
      - `id`: The ID of the category.
    - **Returns**: A task representing the asynchronous operation.
    - **Exceptions**:
      - `InvalidOperationException`: Thrown if the category has associated menu items.
  - `GetCategoryByNameAsync(string name)`: Retrieves a category by its name.
    - **Parameters**:
      - `name`: The name of the category.
    - **Returns**: The `CategoryViewModel` containing category details.

### Menu Item Service
- **Purpose**: Provides business logic for managing menu items.
- **Methods**:
  - `AddMenuItemAsync(CreateMenuItemViewModel model)`: Adds a new menu item to the repository.
    - **Parameters**:
      - `model`: The view model containing menu item details.
    - **Returns**: A task representing the asynchronous operation.
  - `GetMenuItemDetailsByIdAsync(int id)`: Retrieves the details of a menu item by its ID.
    - **Parameters**:
      - `id`: The ID of the menu item.
    - **Returns**: The view model containing menu item details.
    - **Exceptions**:
      - `InvalidOperationException`: Thrown when the menu item is not found.
  - `GetDeletedItemsByCategoryAsync(int? categoryId)`: Retrieves deleted menu items by category ID.
    - **Parameters**:
      - `categoryId`: The category ID.
    - **Returns**: A collection of deleted menu item view models.
  - `UpdateMenuItemAsync(MenuItemEditViewModel model)`: Updates an existing menu item.
    - **Parameters**:
      - `model`: The view model containing updated menu item details.
    - **Returns**: A task representing the asynchronous operation.
    - **Exceptions**:
      - `InvalidOperationException`: Thrown when the menu item is not found.
  - `UnDeleteItemAsync(int id)`: Restores a deleted menu item by its ID.
    - **Parameters**:
      - `id`: The ID of the menu item.
    - **Returns**: A task representing the asynchronous operation.

### Order Service
- **Purpose**: Provides business logic for managing orders.
- **Methods**:
  - `AddItemToCartAsync(int menuItemId, int quantity, OrderViewModel order)`: Adds an item to the cart.
    - **Parameters**:
      - `menuItemId`: The ID of the menu item.
      - `quantity`: The quantity of the item.
      - `order`: The current order.
    - **Returns**: A task representing the asynchronous operation.
  - `AddOrderAsync(OrderViewModel order)`: Adds a new order.
    - **Parameters**:
      - `order`: The order to add.
    - **Returns**: A task representing the asynchronous operation.
  - `UpdateOrderAsync(OrderViewModel orderModel)`: Updates an order's total amount asynchronously.
    - **Parameters**:
      - `orderModel`: The order model containing updated information.
    - **Returns**: A task representing the asynchronous operation.
    - **Exceptions**:
      - `InvalidOperationException`: Thrown if the order is not found.
  - `UpdateQuantityAsync(int orderItemId, int quantity)`: Updates the quantity of an order item asynchronously.
    - **Parameters**:
      - `orderItemId`: The ID of the order item to update.
      - `quantity`: The new quantity for the order item.
    - **Returns**: A task representing the asynchronous operation.
    - **Exceptions**:
      - `InvalidOperationException`: Thrown when the order item is not found.

### Reservation Service
- **Purpose**: Provides business logic for managing reservations.
- **Methods**:
  - `AddReservationAsync(ReservationViewModel model)`: Adds a new reservation asynchronously to the repository.
    - **Parameters**:
      - `model`: The reservation view model containing the details of the reservation.
    - **Returns**: A task representing the asynchronous operation.
  - `CancelReservationAsync(int id)`: Cancels a reservation by setting the `IsCanceled` flag to true.
    - **Parameters**:
      - `id`: The ID of the reservation to cancel.
    - **Returns**: A task representing the asynchronous operation.

---

## Repositories

### Category Repository
- **Purpose**: Manages `Category` entities in the database.
- **Methods**:
  - `AddAsync(Category entity)`: Adds a new `Category` to the database.
    - **Parameters**:
      - `entity`: The category to add.
    - **Returns**: A task representing the asynchronous operation.
    - **Exceptions**:
      - `ArgumentNullException`: Thrown when the category entity is null.
  - `GetAllAsync()`: Retrieves all categories from the database, including their menu items.
    - **Returns**: A list of all categories.
  - `GetByIdAsync(int id)`: Retrieves a category by its ID, including its menu items.
    - **Parameters**:
      - `id`: The ID of the category.
    - **Returns**: The category with the specified ID.
    - **Exceptions**:
      - `KeyNotFoundException`: Thrown when the category is not found.
  - `UpdateAsync(Category entity)`: Updates an existing `Category` in the database.
    - **Parameters**:
      - `entity`: The category to update.
    - **Returns**: A task representing the asynchronous operation.
    - **Exceptions**:
      - `ArgumentNullException`: Thrown when the category entity is null.
  - `DeleteAsync(int id)`: Deletes a category by its ID.
    - **Parameters**:
      - `id`: The ID of the category to delete.
    - **Returns**: A task representing the asynchronous operation.

### Menu Item Repository
- **Purpose**: Manages `MenuItem` entities in the database.
- **Methods**:
  - `AddAsync(MenuItem entity)`: Adds a new `MenuItem` to the database.
    - **Parameters**:
      - `entity`: The menu item to add.
    - **Returns**: A task representing the asynchronous operation.
  - `GetAllAsync()`: Retrieves all menu items from the database.
    - **Returns**: A list of all menu items.
  - `GetByIdAsync(int id)`: Retrieves a menu item by its ID.
    - **Parameters**:
      - `id`: The ID of the menu item.
    - **Returns**: The menu item with the specified ID.
    - **Exceptions**:
      - `KeyNotFoundException`: Thrown when the menu item is not found.
  - `UpdateAsync(MenuItem entity)`: Updates an existing `MenuItem` in the database.
    - **Parameters**:
      - `entity`: The menu item to update.
    - **Returns**: A task representing the asynchronous operation.
  - `DeleteAsync(int id)`: Deletes a menu item by its ID.
    - **Parameters**:
      - `id`: The ID of the menu item to delete.
    - **Returns**: A task representing the asynchronous operation.

### Order Repository
- **Purpose**: Manages `Order` entities in the database.
- **Methods**:
  - `AddAsync(Order entity)`: Adds a new `Order` to the database.
    - **Parameters**:
      - `entity`: The order to add.
    - **Returns**: A task representing the asynchronous operation.
  - `GetAllAsync()`: Retrieves all orders from the database.
    - **Returns**: A list of all orders.
  - `GetByIdAsync(int id)`: Retrieves an order by its ID.
    - **Parameters**:
      - `id`: The ID of the order.
    - **Returns**: The order with the specified ID.
    - **Exceptions**:
      - `KeyNotFoundException`: Thrown when the order is not found.
  - `UpdateAsync(Order entity)`: Updates an existing `Order` in the database.
    - **Parameters**:
      - `entity`: The order to update.
    - **Returns**: A task representing the asynchronous operation.
  - `DeleteAsync(int id)`: Deletes an order by its ID.
    - **Parameters**:
      - `id`: The ID of the order to delete.
    - **Returns**: A task representing the asynchronous operation.

### Reservation Repository
- **Purpose**: Manages `Reservation` entities in the database.
- **Methods**:
  - `AddAsync(Reservation entity)`: Adds a new `Reservation` to the database.
    - **Parameters**:
      - `entity`: The reservation to add.
    - **Returns**: A task representing the asynchronous operation.
  - `GetAllAsync()`: Retrieves all reservations from the database.
    - **Returns**: A list of all reservations.
  - `GetByIdAsync(int id)`: Retrieves a reservation by its ID.
    - **Parameters**:
      - `id`: The ID of the reservation.
    - **Returns**: The reservation with the specified ID.
    - **Exceptions**:
      - `KeyNotFoundException`: Thrown when the reservation is not found.
  - `UpdateAsync(Reservation entity)`: Updates an existing `Reservation` in the database.
    - **Parameters**:
      - `entity`: The reservation to update.
    - **Returns**: A task representing the asynchronous operation.
  - `DeleteAsync(int id)`: Deletes a reservation by its ID.
    - **Parameters**:
      - `id`: The ID of the reservation to delete.
    - **Returns**: A task representing the asynchronous operation.

## Extensions

### Role Initializer
- **Purpose**: Initializes roles and a default admin user in the application.
- **Methods**:
  - `InitializeAsync()`: Asynchronously creates predefined roles and a default administrator account.
    - **Details**:
      - Roles are created using the `RoleManager` provided by ASP.NET Core Identity.
      - A default admin user is created with a predefined email and password if no admin user exists.
    - **Dependencies**:
      - `IServiceProvider`: Used to resolve `RoleManager` and `UserManager`.

---

### ServiceCollectionExtensions
- **Purpose**: Provides extension methods for configuring and adding application-specific services to the dependency injection container.
- **Methods**:
  - `AddApplicationServices(IServiceCollection services)`: Registers core services, such as category, menu, and order services, into the dependency injection container.
    - **Parameters**:
      - `services`: The `IServiceCollection` instance where services will be registered.
    - **Returns**: The updated `IServiceCollection`.
    - **Details**: Includes custom business logic services required by the application.

  - `AddApplicationDbContext(IServiceCollection services, IConfiguration config)`: Configures and registers the application's DbContext.
    - **Parameters**:
      - `services`: The `IServiceCollection` instance where the DbContext will be registered.
      - `config`: The `IConfiguration` instance used to retrieve connection strings.
    - **Returns**: The updated `IServiceCollection`.
    - **Details**: Sets up Entity Framework Core with a connection string defined in the application's configuration files.

  - `AddApplicationIdentity(IServiceCollection services, IConfiguration config)`: Configures and adds ASP.NET Core Identity services for user authentication and authorization.
    - **Parameters**:
      - `services`: The `IServiceCollection` instance where Identity services will be registered.
      - `config`: The `IConfiguration` instance used to retrieve Identity-specific settings.
    - **Returns**: The updated `IServiceCollection`.
    - **Details**:
      - Configures password requirements, user lockout options, and cookie settings.
      - Integrates Identity with Entity Framework Core for user and role management.
