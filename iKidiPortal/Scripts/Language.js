        $(function () {
            $("input[type = 'radio']").click(function () {
                $(this).parents("form").submit(); // post form
            });

        })(jQuery);
     