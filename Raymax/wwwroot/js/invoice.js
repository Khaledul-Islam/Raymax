$(document).ready(function () {
	$(document).on('click', '#checkAll', function () {
		$(".itemRow").prop("checked", this.checked);
	});
	$(document).on('click', '.itemRow', function () {
		if ($('.itemRow:checked').length == $('.itemRow').length) {
			$('#checkAll').prop('checked', true);
		} else {
			$('#checkAll').prop('checked', false);
		}
	});
	var count = $(".itemRow").length;
	var k = 1;
	$(document).on('click', '#addRows', function () {
		count++;
		var htmlRows = '';
		htmlRows += '<tr>';
		htmlRows += '<td><input class="itemRow" type="checkbox"></td>';
		htmlRows += '<td><input name="Products[' + k + '].ProductID"  id="productCode_' + count + '" class="form-control"></td>';
		htmlRows += '<td><input name="Products[' + k + '].Description"  id="productName_' + count + '" class="form-control"></td>';
		htmlRows += '<td><input name="Products[' + k + '].Quantity"  id="quantity_' + count + '" class="form-control quantity"></td>';
		htmlRows += '<td><input name="Products[' + k + '].UnitPrice"  id="price_' + count + '" class="form-control price"></td>';
		htmlRows += '<td><input name="Products[' + k + '].TotalPrice"  id="total_' + count + '" class="form-control total"></td>';
		htmlRows += '</tr>';
		$('#invoiceItem').append(htmlRows);
		k = k + 1;
	});
	$(document).on('click', '#removeRows', function () {
		$(".itemRow:checked").each(function () {
			$(this).closest('tr').remove();
		});
		$('#checkAll').prop('checked', false);
		calculateTotal();
	});
	$(document).on('blur', "[id^=quantity_]", function () {
		calculateTotal();
	});
	$(document).on('blur', "[id^=price_]", function () {
		calculateTotal();
	});
	$(document).on('blur', "#taxRate", function () {
		calculateTotal();
	});
	$(document).on('blur', "#amountPaid", function () {
		var amountPaid = $(this).val();
		var totalAftertax = $('#totalAftertax').val();
		if (amountPaid && totalAftertax) {
			totalAftertax = totalAftertax - amountPaid;
			$('#amountDue').val(totalAftertax);
		} else {
			$('#amountDue').val(totalAftertax);
		}
	});
});
function calculateTotal() {
	var totalAmount = 0;
	$("[id^='price_']").each(function () {
		var id = $(this).attr('id');
		id = id.replace("price_", '');
		var price = $('#price_' + id).val();
		var quantity = $('#quantity_' + id).val();
		if (!quantity) {
			quantity = 1;
		}
		var total = price * quantity;
		$('#total_' + id).val(parseFloat(total));
		totalAmount += total;
	});
	$('#subTotal').val(parseFloat(totalAmount));
	var taxRate = $("#taxRate").val();
	var subTotal = $('#subTotal').val();
	if (subTotal) {
		var taxAmount = subTotal * taxRate / 100;
		$('#taxAmount').val(taxAmount);
		subTotal = parseFloat(subTotal) + parseFloat(taxAmount);
		$('#totalAftertax').val(subTotal);
		var amountPaid = $('#amountPaid').val();
		var totalAftertax = $('#totalAftertax').val();
		if (amountPaid && totalAftertax) {
			totalAftertax = totalAftertax - amountPaid;
			$('#amountDue').val(totalAftertax);
		} else {
			$('#amountDue').val(subTotal);
		}
	}
}

