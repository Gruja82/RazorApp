/////////// Create Page ///////////

// Define counter
let counter = 0;

// Create an array to hold material names added to list
let materialNames = [];

// Define variable that will hold total purchase amount
let total = 0;

// Function for adding rows
function addPurchaseRow() {
    // Create span element
    let span = $('<span id="span' + counter + '"></span>');

    // Define variable that holds selected material from drop down list
    let material = $('#lstMaterials option:selected').text();

    // Define variable that holds purchase code
    let purchase = $('#purchaseCode').val();

    // Define variable that will hold index of selected material
    let materialIndex = $('#lstMaterials option:selected').index();

    // Define variable that holds material quantity
    let quantity = $('#inputQuantity').val();

    // Create input element which holds material name
    let inputMaterial = $('<input class="form-control" id="material' + counter + '" readonly name="PurchaseModel.PurchaseDetailDtos[' + counter + '].MaterialName" value="' + material + '"/>');

    // Create input element which holds material quantity
    let inputQuantity = $('<input class="form-control" id="quantity' + counter + '" readonly name="PurchaseModel.PurchaseDetailDtos[' + counter + '].Qty" value="' + quantity + '"/>');

    // Create input element which holds purchase code
    let inputPurchase = $('<input id="purchase' + counter + '" hidden name="PurchaseModel.PurchaseDetailDtos[' + counter + '].PurchaseCode" value="' + purchase + '"/>');

    // Create button element for deleting current span
    let deleteButton = $('<input type="button" class="btn btn-danger" value="Remove" onclick="removePurchaseRow(' + counter + ');" />');

    // Check if the selected material is already added to list. If it is not, then add material to list
    if (($.inArray(inputMaterial.val(), materialNames)) == -1) {
        if (quantity > 0) {
            // Append inputMaterial, inputQuantity, inputProduct and deleteButton to span
            span.append(inputMaterial);
            span.append(inputQuantity);
            span.append(deleteButton);
            span.append(inputPurchase);

            // Append span an hr to div
            $('#purchaseDetails').append(span);
            $('#purchaseDetails').append('<hr id="hr' + counter + '" />');

            // Add material to materialNames array
            materialNames.push(inputMaterial.val());

            // Variable that represents lstPrice element
            let price = document.getElementById("lstPrice");

            // Increment total variable
            total += quantity * price[materialIndex].value;

            // Round total value to 2 decimal places
            let totalFixed = total.toFixed(2);

            // Appen totalFixed value to inputTotal element
            document.getElementById("inputTotal").value = totalFixed;

            // Increase counter
            counter++;
        }
        else {
            alert('Material quantity must be larger than 0!');
        }
    }
    else {
        alert('Material ' + '"' + inputMaterial.val() + '" is already added to list!');
    }
}

// Function for removing rows
function removePurchaseRow(index) {
    // Define variable that will hold material name
    let materialName = document.getElementById('material' + index).value;

    // Variable that represents lstMaterials drop down list
    let materialList = document.getElementById('lstMaterials');

    // Variable that will hold the index of specific material in lstMaterials
    let materialIndex;

    // Iterate through lstMaterials and fetch index of option element whose value is equal to materialName
    for (let i = 0; i < materialList.length; i++) {
        if (materialList.options[i].value == materialName) {
            materialIndex = i;
        }
    }

    // Variable that will hold price for certain material
    let price = document.getElementById('lstPrice')[materialIndex].value;

    // Variable that represents input element which contains quantity of specific material
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

    // Remove selected material name from materialNames array
    materialNames = $.grep(materialNames, function (value) {
        return value != materialName;
    })
}

/////////// Create Page End ///////////

/////////// Edit Page ///////////

// Array that will contain Material names
let materials = [];

// Array that will contain Material quantities
let quantities = [];

function populatePurchase(material, quantity, total) {
    populatePurchaseRows(material, quantity);
    populatePurchaseTotal(total);
}

// Function for adding materials to materials array that are alredy contained in model
function addPurchaseMaterials(material) {
    materials.push(material);
}

// Function for adding material quantities to quantities array that are already contained in model
function addPurchaseQuantities(quantity) {
    quantities.push(quantity);
}

// Function for creating HTML elements that represent currently present values in model
function populatePurchaseRows(material, quantity){
    addPurchaseMaterials(material);
    addPurchaseQuantities(quantity);

    // Variable that represents purchase code
    let purchase = $('#purchaseCode').val();

    // Create span element
    let span = $('<span id="span' + counter + '"></span>');

    // Create input element that holds material name
    let inputMaterial = $('<input class="form-control" id="material' + counter + '" readonly name="PurchaseModel.PurchaseDetailDtos[' + counter + '].MaterialName" value="' + material + '"/>');

    // Create input element which holds material quantity
    let inputQuantity = $('<input id="quantity' + counter + '" class="form-control" readonly name="PurchaseModel.PurchaseDetailDtos[' + counter + '].Qty" value="' + quantity + '"/>');

    // Create input element that holds purchase code
    let inputPurchase = $('<input id="purchase' + counter + '" hidden name="PurchaseModel.PurchaseDetailDtos[' + counter + '].PurchaseCode" value="' + purchase + '"/>');

    // Create button element for deleting current span
    let deleteButton = $('<input type="button" class="btn btn-danger" value="Remove" onclick="removePurchaseEditRow(' + counter + ');" />');

    // Append inputMaterial, inputQuantity, inputPurchase and deleteButton to span
    span.append(inputMaterial);
    span.append(inputQuantity);
    span.append(deleteButton);
    span.append(inputPurchase);

    // Append span to div
    $('#purchaseDetails').append(span);
    $('#purchaseDetails').append('<hr id="hr' + counter + '" />');

    // Append material names to materialNames
    materialNames.push(inputMaterial.val());

    // Increase counter
    counter++;
}

// Function for adding new rows
function addPurchaseEditRow() {
    // Create span element
    let span = $('<span id="span' + counter + '"></span>');

    // Define variable that holds selected material from drop down list
    let material = $('#lstMaterials option:selected').text();

    // Define variable that holds purchase code
    let purchase = $('#purchaseCode').val();

    // Define variable that will hold index of selected material
    let materialIndex = $('#lstMaterials option:selected').index();

    // Define variable that holds material quantity
    let quantity = $('#inputQuantity').val();

    // Create input element that holds material name
    let inputMaterial = $('<input class="form-control-sm" id="material' + counter + '" readonly name="PurchaseModel.PurchaseDetailDtos[' + counter + '].MaterialName" value="' + counter + '"/>');

    // Create input element which holds material quantity
    let inputQuantity = $('<input id="quantity' + counter + '" class="form-control" readonly name="PurchaseModel.PurchaseDetailDtos[' + counter + '].Qty" value="' + counter + '"/>');

    // Create input element which holds purchase code
    let inputPurchase = $('<input id="purchase' + counter + '" hidden name="PurchaseModel.OrderDetailDtos[' + counter + '].PurchaseCode" value="' + order + '"/>');

    // Create button element for deleting current span
    let deleteButton = $('<input type="button" class="btn btn-danger" value="Remove" onclick="removePurchaseEditRow(' + counter + ');" />');

    // Check if the selected material is already added to list
    if (($.inArray(inputMaterial.val(), materialNames)) == -1) {
        if (quantity > 0) {
            // Append inputMaterial, inputQuantity, inputPurchase and deleteButton to span
            span.append(inputMaterial);
            span.append(inputQuantity);
            span.append(deleteButton);
            span.append(inputPurchase);

            // Append span to div
            $('#purchaseDetails').append(span);
            $('#purchaseDetails').append('<hr id="hr' + counter + '" />');

            // Add material to materialNames array
            materialNames.push(inputMaterial.val());

            // Variable that represents lstPrice element
            let price = document.getElementById("lstPrice");

            // Increment total variable
            total += quantity * price[materialIndex].value;

            // Round total value to 2 decimal places
            let totalFixed = total.toFixed(2);

            // Append totalFixed value to inputTotal element
            document.getElementById("inputTotal").value = totalFixed;

            // Increase counter
            counter++;
        }
        else {
            alert('Material quantity must be larger than 0!');
        }
    }
    else {
        alert('Material ' + '"' + inputMaterial.val() + '" is already added to list!')
    }
}

// Function for removing rows
function removePurchaseEditRow(index) {
    let removeItem = $('#material' + index).val();

    // Define variable that will hold Material name
    let materialName = document.getElementById('material' + index).value;

    // Variable that represents lstMaterials drop down list
    let materialList = document.getElementById('lstMaterials');

    // Variable that will hold the index of certain material in lstMaterials
    let materialIndex;

    // Iterate through lstMaterials and fetch index of option element whose value is equal to materialName
    for (let i = 0; i < materialList.length; i++) {
        if (materialList.options[i].value == materialName) {
            materialIndex = i;
        }
    }

    // Variable that will hold price for certain material
    let price = document.getElementById('lstPrice')[materialIndex].value;

    // Variable that represents input element which contains quantity of certain material
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

    // Remove selected material name from materialNames array
    materialNames = $.grep(materialNames, function (value) {
        return value != removeItem;
    });
}

// Function for populating total variable
function populatePurchaseTotal(totalValue) {
    total = totalValue;
    var totalFixed = total.toFixed(2);

    // Append totalFixed value to inputTotal element
    document.getElementById("inputTotal").value = totalFixed;
}

////////// Edit Page End //////////