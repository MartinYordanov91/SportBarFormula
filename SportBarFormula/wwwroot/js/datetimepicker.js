$(function ($) {
    // Комбиниране на стойностите от селект листовете за дата и час в едно поле
    $('.date-part').on('change', function () {
        var day = $('#Day').val().padStart(2, '0');
        var month = $('#Month').val().padStart(2, '0');
        var year = $('#Year').val();
        var hour = $('#Hour').val().padStart(2, '0');
        var minute = $('#Minute').val().padStart(2, '0');

        var formattedDate = `${day}-${month}-${year} ${hour}:${minute}`;
        $('input[name="ReservationDate"]').val(formattedDate);
    });

    // Инициализиране на Bootstrap dropdown менюто
    $('.dropdown-toggle').dropdown();

    $('#NumberOfGuests').on('input', function () {
        var numGuests = $(this).val();
        if (numGuests > 10) {
            alert('За резервация за повече от 10 човека, моля свържете се с нас.');
            $(this).val('');
        }
    });
});
