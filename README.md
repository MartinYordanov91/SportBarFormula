# SportBarFormula.Core Documentation

## Overview
This document provides a summary of the main classes, methods, and their descriptions for the SportBarFormula.Core project.

---

## Services

### CategoryService
#### Methods:
- **GetAllCategoriesAsync**
  - **Description**: Retrieves all categories.
  - **Returns**: A collection of `CategoryViewModel` containing category details.

- **AddCategoryAsync(CategoryViewModel)**
  - **Description**: Adds a new category.
  - **Parameters**:
    - `model`: The view model containing category details to add.
  - **Returns**: A task representing the asynchronous operation.

- **GetCategoryByIdAsync(int id)**
  - **Description**: Retrieves a category by its ID.
  - **Parameters**:
    - `id`: The ID of the category.
  - **Returns**: The view model containing category details.
  - **Throws**:
    - `InvalidOperationException`: When no categories are found.

- **UpdateCategoryAsync(CategoryViewModel)**
  - **Description**: Updates an existing category.
  - **Parameters**:
    - `model`: The view model containing updated category details.
  - **Returns**: A task representing the asynchronous operation.

- **DeleteCategoryAsync(int id)**
  - **Description**: Deletes a category by its ID if it has no associated menu items.
  - **Parameters**:
    - `id`: The ID of the category.
  - **Returns**: A task representing the asynchronous operation.
  - **Throws**:
    - `InvalidOperationException`: When no categories are found.
    - `InvalidOperationException`: When the category has associated menu items.

---

### MenuItemService
#### Methods:
- **AddMenuItemAsync(CreateMenuItemViewModel)**
  - **Description**: Adds a new menu item to the repository.
  - **Parameters**:
    - `model`: The view model containing menu item details.
  - **Returns**: A task representing the asynchronous operation.

- **GetMenuItemDetailsByIdAsync(int id)**
  - **Description**: Retrieves the details of a menu item by its ID.
  - **Parameters**:
    - `id`: The ID of the menu item.
  - **Returns**: The view model containing menu item details.
  - **Throws**:
    - `InvalidOperationException`: When the menu item is not found.

---

### OrderService
#### Methods:
- **AddOrderAsync(OrderViewModel)**
  - **Description**: Adds a new order.
  - **Parameters**:
    - `order`: The order to add.

- **RemoveItemFormCartAsync(int orderItemId)**
  - **Description**: Removes an item from the cart asynchronously.
  - **Parameters**:
    - `orderItemId`: The ID of the order item to remove.
  - **Returns**: A task representing the asynchronous operation.
  - **Throws**:
    - `InvalidOperationException`: When the order item is not found.

---

### ReservationService
#### Methods:
- **AddReservationAsync(ReservationViewModel)**
  - **Description**: Adds a new reservation asynchronously to the repository.
  - **Parameters**:
    - `model`: The reservation view model containing the details of the reservation to be added.
  - **Returns**: A task representing the asynchronous operation.

- **CancelReservationAsync(int id)**
  - **Description**: Cancels the reservation by setting the IsCanceled flag to true.
  - **Parameters**:
    - `id`: The ID of the reservation to cancel.
  - **Returns**: A task representing the asynchronous operation.

---

## ViewModels

### OrderItemViewModel
- **OrderItemId**: Gets or sets the unique identifier of the order item.
- **MenuItemId**: Gets or sets the unique identifier of the menu item.
- **MenuItemName**: Gets or sets the name of the menu item.
- **Quantity**: Gets or sets the quantity of the menu item.
- **Price**: Gets or sets the price of the menu item at the time of the order.

### OrderViewModel
- **OrderId**: Gets or sets the unique identifier of the order.
- **UserId**: Gets or sets the identifier of the user who placed the order.
- **OrderDate**: Gets or sets the date of the order in string format.
- **TotalAmount**: Gets or sets the total amount of the order.
- **Status**: Gets or sets the status of the order (Draft, Completed, or Canceled).
- **OrderItems**: Gets or sets the list of items in the order.
