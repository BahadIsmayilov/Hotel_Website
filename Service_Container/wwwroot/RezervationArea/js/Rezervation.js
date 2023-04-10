<script>
    function editStatus(productId) {
        $.ajax({
            url: '/Rezervation/UpdateOrderStatus/' + productId,
            type: 'POST',
            dataType: 'json',
            success: function (result) {
                if (result.success) {
                    $('#status').text(result.status);
                } else {
                    alert(result.message);
                }
            },
            error: function (xhr, status, error) {
                alert('Error: ' + error);
            }
        })
    }
</script>

      
