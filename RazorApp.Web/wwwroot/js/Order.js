/////////// Create Page ///////////

// Define counter
let counter = 0;

// Create an array to hold product names added to list
let productNames = [];

// Define variable that will hold total order amount
let total = 0;

// Function for adding rows
function addOrderRow() {
    // Create span element
    let span = $('<span id="span' + counter + '"></span>');

    // Define variable that holds selected product from drop down list
    let product = $('#lstProducts option:selected').text();

    // Define variable that holds order code
    let order = $('#orderCode').val();

    // Define variable that will hold index of selected product
    let productIndex = $('#lstProducts option:selected').index();

    // Define variable that holds product quantity
    let quantity = $('#inputQuantity').val();

    // Create input element which holds product name
    let inputProduct = $('<input class="form-control" id="product' + counter + '" readonly name="OrderModel.OrderDetailDtos[' + counter + '].ProductName" value="' + product + '"/>');

    // Create input element which holds product quantity
    let inputQuantity = $('<input class="form-control" id="quantity' + counter + '" readonly name="OrderModel.OrderDetailDtos[' + counter + '].Qty" value="' + quantity + '"/>');

    // Create input element which holds order code
    let inputOrder = $('<input id="order' + counter + '" hidden name="OrderModel.OrderDetailDtos[' + counter + '].OrderCode" value="' + order + '"/>');

    // Create button element for deleting current span
    let deleteButton = $('<input type="button" class="btn btn-danger" value="Remove" onclick="removeOrderRow(' + counter + ');" />');

    // Check if the selected product is already added to list. If it is not, then add product to list
    if (($.inArray(inputProduct.val(), productNames)) == -1) {
        if (quantity > 0) {
            // Append inputMaterial, inputQuantity, inputProduct and deleteButton to span
            span.append(inputProduct);
            span.append(inputQuantity);
            span.append(deleteButton);
            span.append(inputOrder);

            // Append span an hr to div
            $('#orderDetails').append(span);
            $('#orderDetails').append('<hr id="hr' + counter + '" />');

            // Add product to productNames array
            productNames.push(inputProduct.val());

            // Variable that represents lstPrice element
            let price = document.getElementById("lstPrice");

            // Increment total variable
            total += quantity * price[productIndex].value;

            // Round total value to 2 decimal places
            let totalFixed = total.toFixed(2);

            // Appen totalFixed value to inputTotal element
            document.getElementById("inputTotal").value = totalFixed;

            // Increase counter
            counter++;
        }
        else {
            alert('Product quantity must be larger than 0!');
        }
    }
    else {
        alert('Product ' + '"' + inputProduct.val() + '" is already added to list!');
    }
}

// Function for removing rows
function removeOrderRow(index) {
    // Define variable that will hold product name
    let productName = document.getElementById('product' + index).value;

    // Variable that represents lstProducts drop down list
    let productList = document.getElementById('lstProducts');

    // Variable that will hold the index of specific product in lstProducts
    let productIndex;

    // Iterate through lstProducts and fetch index of option element whose value is equal to productName
    for (let i = 0; i < productList.length; i++) {
        if (productList.options[i].value == productName) {
            productIndex = i;
        }
    }

    // Variable that will hold price for certain product
    let price = document.getElementById('lstPrice')[productIndex].value;

    // Variable that represents input element which contains quantity of specific product
    let inputQuantity = document.getElementById('quantity' + index).value;

    // Reduce total variable value
    total -= price * inputQuantity;

    // Round total value to 2 decimal places
    let totalFixed = total.toFixed(2);

    // Append totalFixed value to inputTotal element
    document.getElementById('inputTotal').value = totalFixed;

    // Remove current span element
    $('#span' + index).remove();
    $('#hr' + index).remove();

    // Decrease counter
    counter--;

    // Remove selected product name from productNames array
    productNames = $.grep(productNames, function (value) {
        return value != productName;
    })
}

/////////// Create Page End ///////////

/////////// Edit Page ///////////

// Array that will contain Product names
let products = [];

// Array that will contain Product quantities
let quantities = [];

function populateOrder(product, quantity, total) {
    populateOrderRows(product, quantity);
    populateOrderTotal(total);
}

// Function for adding products to products array that are alreay contained in model
function addOrderProducts(product) {
    products.push(product);
}

// Function for adding product quantities to quantities array that are already contained in model
function addOrderQuantities(quantity) {
    quantities.push(quantity);
}

// Function for creating HTML elements that represent currently present values in model
function populateOrderRows(product, quantity) {
    addOrderProducts(product);
    addOrderQuantities(quantity);

    // Variable that represents order code
    let order = $('#orderCode').val();

    // Create span element
    let span = $('<span id="span' + counter + '"></span>');

    // Create input element that holds product name
    let inputProduct = $('<input class="form-control" id="product' + counter + '" readonly name="OrderModel.OrderDetailDtos[' + counter + '].ProductName" value="' + product + '"/>');

    // Create input element which holds product quantity
    let inputQuantity = $('<input id="quantity' + counter + '" class="form-control" readonly name="OrderModel.OrderDetailDtos[' + counter + '].Qty" value="' + quantity + '"/>');

    // Create input element that holds order code
    let inputOrder = $('<input id="order' + counter + '" hidden name="OrderModel.OrderDetailDtos[' + counter + '].OrderCode" value="' + order + '"/>');

    // Create button element for deleting current span
    let deleteButton = $('<input type="button" class="btn btn-danger" value="Remove" onclick="removeOrderEditRow(' + counter + ');" />');

    // Append inputProduct, inputQuantity, inputOrder and deleteButton to span
    span.append(inputProduct);
    span.append(inputQuantity);
    span.append(deleteButton);
    span.append(inputOrder);

    // Append span to div
    $('#orderDetails').append(span);
    $('#orderDetails').append('<hr id="hr' + counter + '" />');

    // Append product names to productNames
    productNames.push(inputProduct.val());

    // Increase counter
    counter++;
}

// Function for adding new rows
function addOrderEditRow() {
    // Create span element
    let span = $('<span id="span' + counter + '"></span>');

    // Define variable that holds selected product from drop down list
    let product = $('#lstProducts option:selected').text();

    // Define variable that holds order code
    let order = $('#orderCode').val();

    // Define variable that will hold index of selected product
    let productIndex = $('#lstProducts option:selected').index();

    // Define variable that holds product quantity
    let quantity = $('#inputQuantity').val();

    // Create input element that holds product name
    let inputProduct = $('<input class="form-control-sm" id="product' + counter + '" readonly name="OrderModel.OrderDetailDtos[' + counter + '].ProductName" value="' + counter + '"/>');

    // Create input element which holds product quantity
    let inputQuantity = $('<input id="quantity' + counter + '" class="form-control" readonly name="OrderModel.OrderDetailDtos[' + counter + '].Qty" value="' + counter + '"/>');

    // Create input element which holds order code
    let inputOrder = $('<input id="order' + counter + '" hidden name="OrderModel.OrderDetailDtos[' + counter + '].OrderCode" value="' + order + '"/>');

    // Create button element for deleting current span
    let deleteButton = $('<input type="button" class="btn btn-danger" value="Remove" onclick="removeOrderEditRow(' + counter + ');" />');

    // Check if the selected product is already added to list
    if (($.inArray(inputProduct.val(), productNames)) == -1) {
        if (quantity > 0) {
            // Append inputProduct, inputQuantity, inputOrder and deleteButton to span
            span.append(inputProduct);
            span.append(inputQuantity);
            span.append(deleteButton);
            span.append(inputOrder);

            // Append span to div
            $('#orderDetails').append(span);
            $('#orderDetails').append('<hr id="hr' + counter + '" />');

            // Add product to productNames array
            productNames.push(inputProduct.val());

            // Variable that represents lstPrice element
            let price = document.getElementById("lstPrice");

            // Increment total variable
            total += quantity * price[productIndex].value;

            // Round total value to 2 decimal places
            let totalFixed = total.toFixed(2);

            // Append totalFixed value to inputTotal element
            document.getElementById("inputTotal").value = totalFixed;

            // Increase counter
            counter++;
        }
        else {
            alert('Product quantity must be larger than 0!');
        }
    }
    else {
        alert('Product ' + '"' + inputProduct.val() + '" is already added to list!')
    }
}

// Function for removing rows
function removeOrderEditRow(index) {
    let removeItem = $('#product' + index).val();

    // Define variable that will hold Product name
    let productName = document.getElementById('product' + index).value;

    // Variable that represents lstProducts drop down list
    let productList = document.getElementById('lstProducts');

    // Variable that will hold the index of certain product in lstProducts
    let productIndex;

    // Iterate through lstProducts and fetch index of option element whose value is equal to productName
    for (let i = 0; i < productList.length; i++) {
        if (productList.options[i].value == productName) {
            productIndex = i;
        }
    }

    // Variable that will hold price for certain product
    let price = document.getElementById('lstPrice')[productIndex].value;

    // Variable that represents input element which contains quantity of certain product
    let inputQuantity = document.getElementById('quantity' + index).value;

    // Reduce total variable value
    total -= price * inputQuantity;

    // Round total value to 2 decimal places
    let totalFixed = total.toFixed(2);

    // Append totalFixed value to inputTotal element
    document.getElementById('inputTotal').value = totalFixed;

    // Remove current span element
    $('#span' + index).remove();
    $('#hr' + index).remove();

    // Decrease counter
    counter--;

    // Remove selected product name from productNames array
    productNames = $.grep(productNames, function (value) {
        return value != removeItem;
    });
}

// Function for populating total variable
function populateOrderTotal(totalValue) {
    total = totalValue;
    var totalFixed = total.toFixed(2);

    // Append totalFixed value to inputTotal element
    document.getElementById("inputTotal").value = totalFixed;
}

////////// Edit Page End //////////