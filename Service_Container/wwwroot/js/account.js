$(function () {
    $("#countries").change(function () {
        var countryId = $(this).val();

        if (countryId) {
            $.ajax({
                url: "/Account/LoadCitiesByCountryId?countryId=" + countryId,
                type: "POST",
                success: function (res) {
                    $("#CityId").html(res);
                    $("#CityId").prepend("<option value=''>Select City</option>")
                }
            });
        }
    });
});


