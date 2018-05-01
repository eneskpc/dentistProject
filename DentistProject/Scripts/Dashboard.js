function twoDigits(number) {
    return ('0' + number).slice(-2);
}

function fromTurkish(minuteTime) {
    var cekimEki = ['dan', 'den', 'den', 'den', 'den', 'den', 'dan', 'den', 'den', 'dan'];
    var cekimEkiBuyuk = ['dan', 'den', 'dan', 'dan', 'den'];

    return minuteTime > 9 && minuteTime % 10 == 0 ? cekimEkiBuyuk[(minuteTime / 10) - 1] : cekimEki[minuteTime % 10];
}

function toTurkish(minuteTime) {
    var cekimEki = ['a', 'e', 'ye', 'e', 'e', 'e', 'ya', 'ye', 'e', 'a'];
    var cekimEkiBuyuk = ['a', 'e', 'ye', 'e', 'e'];

    return minuteTime > 9 && minuteTime % 10 == 0 ? cekimEkiBuyuk[(minuteTime / 10) - 1] : cekimEki[minuteTime % 10];
}

function changeDateRange(pickerStart, pickerEnd, element, isTimePicker, isNoRange) {
    var selectedStartDateTime = '<strong class="text-info">' + moment(pickerStart).format('LL' + (isTimePicker ? 'L' : '')) + '</strong>';
    var selectedEndDateTime = '<strong class="text-warning">' + moment(pickerEnd).format('LL' + (isTimePicker ? 'L' : '')) + '</strong>';

    pickerStart = pickerStart ? pickerStart : moment();
    pickerEnd = pickerEnd ? pickerEnd : moment();

    element.find('.range-view').html(selectedStartDateTime + (!isNoRange ? ('\'' + fromTurkish(pickerStart._d.getMinutes()) + ' ' +
        selectedEndDateTime + '\'' + toTurkish(pickerEnd._d.getMinutes()) + ' kadar') : ''));
    if (element.prev().prop('tagName') === 'INPUT') {
        element.prev().val(moment(pickerStart).format('DD.MM.YYYY hh:mm,') + moment(pickerEnd).format('DD.MM.YYYY hh:mm'));
    }
}

function createDateRange(selector, start, end, isTimePicker, isNoRange) {

    $(selector).each(function (i, val) {

        var thisElement = $(this);

        if (thisElement.prop('tagName') !== 'INPUT') {
            return false;
        }

        thisElement.attr('type', 'hidden');
        if (thisElement.next('#daterange' + i).length == 0) {
            thisElement.after('<div id="daterange' + i + '" class="form-control"><i class="fa fa-calendar fa-fw mr-2"></i><div class="d-inline-block range-view"></div></div>');
        }
        var thisDateRangePicker = thisElement.next('#daterange' + i);

        thisDateRangePicker.daterangepicker({
            autoApply: true,
            singleDatePicker: isNoRange,
            timePicker: isTimePicker,
            timePicker24Hour: isTimePicker,
            startDate: start ? start : moment(),
            endDate: end ? end : moment(),
            locale: {
                format: 'DD.MM.YYYY HH:mm',
                separator: ',',
                applyLabel: 'Uygula',
                cancelLabel: "İptal"
            },
        }, function (pickerStart, pickerEnd, label) { changeDateRange(pickerStart, pickerEnd, thisDateRangePicker, isTimePicker, isNoRange) });

        changeDateRange(start, end, thisDateRangePicker, isTimePicker, isNoRange);
    });

}

$(document).ready(function () {
    var randevuEkle = $('#randevuEkle');
    var scheduler = $('#calendar').fullCalendar({
        themeSystem: 'bootstrap4',
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        allDayDefault: false,
        defaultDate: '2018-03-12',
        navLinks: true,
        selectable: true,
        selectHelper: true,
        select: function (start, end) {
            createDateRange('#randevuZamani', start, end);
            randevuEkle.modal('show');
        },
        editable: true,
        eventLimit: true,
        events: function (start, end, timezone, callback) {
            $.ajax({
                url: '/webservices/getappointments',
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                    var events = [];
                    $.each(data, function () {
                        var appointmentDateUnix = this.AppointmentDate.match(/([0-9]{1,})/g);
                        events.push({
                            id: this.TCNo,
                            title: this.NameSurname,
                            start: moment(appointmentDateUnix * 1),
                            end: moment(appointmentDateUnix * 1).add(30, 'minutes'),
                            backgroundColor: (moment(appointmentDateUnix * 1).fromNow().indexOf('önce') > -1 ? '#f96332' : '#f8f9fa'),
                            color: '#000'
                        });
                    });
                    console.log(data);
                    callback(events);
                }
            });
        }
    });

    $('input[name=selectPatient]').selectize({
        valueField: 'tcNo',
        labelField: 'name',
        searchField: ['name', 'tcNo', 'birthDate'],
        create: false,
        maxItems: 1,
        render: {
            option: function (item, escape) {
                return '<div class="media">' +
                    '<img class="mr-3" src="' + escape(item.photoUrl) + '" alt="' + escape(item.name) + '">' +
                    '<div class="media-body">' +
                    '<h5 class="mt-0">' + escape(item.name) + '</h5>' +
                    '<span class="mr-1">' + escape(item.tcNo) + '</span></span><span class="description">' + escape(item.birthDate) + '</span></div></div>';
            },
            item: function (item, escape) {
                return '<div class="d-inline-block"><div class="media" style="display:flex;">' +
                    '<img class="mr-3" style="width:32px;" src="' + escape(item.photoUrl) + '" alt="' + escape(item.name) + '">' +
                    '<div class="media-body">' +
                    '<h5 class="m-0">' + escape(item.name) + '</h5></div></div></div>';
            }
        },
        load: function (query, callback) {
            if (!query.length) return callback();
            var patients = [
                {
                    name: 'Enes Kapucu',
                    photoUrl: 'https://dummyimage.com/64x64/000/fff.png',
                    birthDate: moment('16.06.1994', 'DD.MM.YYYY').format('LL'),
                    tcNo: '12345678901'
                },
                {
                    name: 'Emirhan Dağlıoğlu',
                    photoUrl: 'https://dummyimage.com/64x64/000/fff.png',
                    birthDate: moment('06.05.1996', 'DD.MM.YYYY').format('LL'),
                    tcNo: '01123456789'
                }
            ];
            callback(patients);
        }
    });

    $(document).on('click', '#randevuKaydet', function () {
        var eventData = {
            title: $(this).parents('.modal').find('input[name=selectPatient]').val(),
            start: moment($(this).parents('.modal').find('input#randevuZamani').val().split(',')[0], 'DD.MM.YYYY hh:mm'),
            end: moment($(this).parents('.modal').find('input#randevuZamani').val().split(',')[1], 'DD.MM.YYYY hh:mm')
        };
        $('#calendar').fullCalendar('renderEvent', eventData, true);
        $('#calendar').fullCalendar('unselect');
    });

    $(document).on('click', 'a#newPatient', function () {
        $(this).parents('.form-group').addClass('d-none');
        $('a#existingPatient').parents('.form-group').removeClass('d-none');
        $('input[name=TCNo]').parents('.form-group').removeClass('d-none');
        $('input[name=birthDate]').parents('.form-group').removeClass('d-none');
        $('select[name=selectGender]').parents('.form-group').removeClass('d-none');
        $('input[name=userType]').val('new');
    });

    $(document).on('click', 'a#existingPatient', function () {
        $(this).parents('.form-group').addClass('d-none');
        $('a#newPatient').parents('.form-group').removeClass('d-none');
        $('input[name=TCNo]').parents('.form-group').addClass('d-none');
        $('input[name=birthDate]').parents('.form-group').addClass('d-none');
        $('select[name=selectGender]').parents('.form-group').addClass('d-none');
        $('input[name=userType]').val('existing');
    });

    createDateRange("[name=birthDate]", moment().subtract(5, 'years'), null, false, true);

    randevuEkle.on('show.bs.modal', function (e) {
        $('a#existingPatient').trigger('click');
    })
});