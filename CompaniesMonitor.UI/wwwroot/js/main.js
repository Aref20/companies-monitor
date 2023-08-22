//make the select box single select for companies type
//document.querySelectorAll("#CompaniesType")[0].removeAttribute("multiple");



    function deleteRow(button) {
        var row = button.closest('.row');
    row.remove();
    }

    function addDocumentRow() {
        // Clone the last row and clear its input values
        var newRow = $('.container.row:last').clone();
    newRow.find('input, select').val('');

    // Append the new row to the container
        $('#line').append(newRow);
    }
