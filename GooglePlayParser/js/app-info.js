window.onload = () => {
    $('#package-find').click(() => {
        var val = $('#package-name')[0].value;

        $('#error-msg').hide();
        $('#package-name').hide();
        $('#package-find').hide();


        $.ajax({
            type: 'GET',
            url: '/api/GooglePlay?id=' + val
        }).done(res => {
            console.log('Getted data', res);
            // при ошибке
            if (res.error) {
                $('#error-msg')[0].innerText = res.error;
                $('#error-msg').show();
                return;
            }
            // вывод данных
            $('#app-list')[0].innerHTML = "";
            addApplication(res.PackageName, res)

        }).fail(res => {
            console.log('fail', res);
            $('#error-msg')[0].innerText = res;
            $('#error-msg').show();
            //$('#app-list #loading').text('Ошибка при загрузке данных: ' + res);
        }).always(res => {
            $('#package-name')[0].value = '';
            $('#package-name').show();
            $('#package-find').show();
        });
    });
}