function SubmitAjax() {
    $(document).ready(function () {
        $('#updatemodel').click(function () {
            var model = $("#myform").serialize();
            $.ajax({
                url: '/Home/GetFamilyDetails',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: model,
                success: function (result) {
                    var firstname = $("#fname").val();
                    alert(firstname);
                }
            });

        });
    });
}