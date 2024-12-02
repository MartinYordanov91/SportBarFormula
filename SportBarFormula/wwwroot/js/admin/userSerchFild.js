/// <summary>
/// Adds event listeners for search functionality on the user table.
/// </summary>

document.addEventListener('DOMContentLoaded', function () {
    var searchInput = document.getElementById('searchInput');
    var tableRows = document.querySelectorAll('#usersTable tbody tr');

    /// <summary>
    /// Filters table rows based on the search input.
    /// </summary>
    searchInput.addEventListener('keyup', function () {
        var searchValue = searchInput.value.toLowerCase();

        tableRows.forEach(function (row) {
            var emailCell = row.cells[1].textContent.toLowerCase();

            if (emailCell.includes(searchValue)) {
                row.style.display = '';
            } else {
                row.style.display = 'none';
            }
        });
    });
});
