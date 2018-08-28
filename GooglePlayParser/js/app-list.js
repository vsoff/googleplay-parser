var listItems = {};

function addApplication(key, data) {
    if (listItems[key])
        return;
    listItems[key] = data;

    var colStyle = 'col-md-2'

    // cols elements
    var el;
    var c1 = document.createElement('div');
    el = document.createElement('b');
    el.classList = 'app-icon';
    el.innerText = data.Name;
    c1.appendChild(el);
    el = document.createElement('i');
    el.classList = 'app-icon';
    el.innerText = key;
    c1.classList = colStyle;
    c1.appendChild(document.createElement('br'));
    c1.appendChild(el);

    var c2 = document.createElement('div');
    el = document.createElement('img');
    el.classList = 'app-icon';
    el.src = data.Icon;
    c2.classList = colStyle;
    c2.appendChild(el);

    var c3 = document.createElement('div');
    c3.classList = colStyle;
    el = document.createElement('b');
    el.innerText = 'Описание:'
    c3.appendChild(el);
    el = document.createElement('p');
    el.classList = 'preview';
    el.innerText = data.Description.length > 100 ? data.Description.substring(0, 100) + '...' : data.Description;
    c3.appendChild(el);

    var c4 = document.createElement('div');
    c4.classList = colStyle;
    if (data.InternalPrice) {
        el = document.createElement('p');
        el.innerText = 'Внутренние покупки: ' + data.InternalPrice
        c4.appendChild(el);
    }
    el = document.createElement('p');
    el.innerText = 'Кол-во установок: ' + data.InstallCount
    c4.appendChild(el);
    el = document.createElement('p');
    el.innerText = 'Кол-во оценок: ' + data.RatingCount
    c4.appendChild(el);
    el = document.createElement('p');
    el.innerText = 'Средняя оценка: ' + data.Rating
    c4.appendChild(el);
    el = document.createElement('p');
    el.innerText = 'Цена: ' + (data.Price == 0 ? 'бесплатно' : data.Price)
    c4.appendChild(el);

    var c5 = document.createElement('div');
    c5.classList = colStyle;
    el = document.createElement('b');
    el.innerText = 'Что нового:'
    c5.appendChild(el);
    el = document.createElement('p');
    el.classList = 'preview';
    if (data.WhatsNew)
        el.innerText = data.WhatsNew.length > 100 ? data.WhatsNew.substring(0, 100) + '...' : data.WhatsNew;
    else
        el.innerText = 'Новостей нет.';
    c5.appendChild(el);

    var c6 = document.createElement('div');
    c6.classList = colStyle;
    el = document.createElement('b');
    el.innerText = 'Email'
    c6.appendChild(el);
    el = document.createElement('p');
    el.classList = 'preview';
    el.innerText = data.Email
    c6.appendChild(el);

    // row element
    var row = document.createElement('div');
    row.classList = 'row app-item';
    row.setAttribute('name', key);
    console.log(key);

    row.setAttribute('data-toggle', 'modal');
    row.setAttribute('data-target', '#myModal');

    row.appendChild(c2);
    row.appendChild(c1);
    row.appendChild(c3);
    row.appendChild(c4);
    row.appendChild(c5);
    row.appendChild(c6);

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