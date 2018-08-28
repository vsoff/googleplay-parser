var listItems = {};

function addApplication(key, data) {
    if (listItems[key])
        return;
    listItems[key] = data;

    // cols elements
    var c1 = document.createElement('div');
    var i = document.createElement('i');
    i.classList = 'app-icon';
    i.innerText = key;
    c1.classList = 'col-md-3';
    c1.appendChild(i);

    var c2 = document.createElement('div');
    var b = document.createElement('b');
    b.classList = 'app-icon';
    b.innerText = data.Name;
    c2.classList = 'col-md-3';
    c2.appendChild(b);

    var c3 = document.createElement('div');
    var img = document.createElement('img');
    img.classList = 'app-icon';
    img.src = data.Icon;
    c3.classList = 'col-md-3';
    c3.appendChild(img);

    var c4 = document.createElement('div');
    c4.classList = 'col-md-3';
    c4.innerText = data.Description.substring(0, 100);

    // row element
    var row = document.createElement('div');
    row.classList = 'row app-item';
    row.setAttribute('name', key);
    console.log(key);

    row.setAttribute('data-toggle', 'modal');
    row.setAttribute('data-target', '#myModal');

    row.appendChild(c3);
    row.appendChild(c1);
    row.appendChild(c2);
    row.appendChild(c4);

    row.onclick = function () {
        var item = listItems[this.getAttribute('name')];

        $('h4#supertitle-modal')[0].innerText = "Скриншоты " + item.Name;
        $('#screenshots-modal')[0].innerHTML = "";

        var screens = item.Screenshots;
        for (var s in screens) {
            var scr = document.createElement('img');
            scr.src = screens[s];
            $('#screenshots-modal')[0].appendChild(scr);
        }

    };

    $('#app-list')[0].appendChild(row);
}

window.onload = () => {
    $.ajax({
        type: 'GET',
        url: '/api/GooglePlay'
    }).done(res => {
        console.log('Getted data', res);
        $('#app-list #loading').hide();
        for (var key in res)
            addApplication(key, res[key]);
    }).fail(res => {
        console.log('fail', res);
        $('#app-list #loading').text('Ошибка при загрузке данных: ' + res);
    });
}