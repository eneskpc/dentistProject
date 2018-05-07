function twoDigits(number) {
    return ('0' + number).slice(-2);
}

function createDatePicker(selector, momentDate) {
    $(selector).next('.form-control').remove();
    $(selector).addClass('d-none');
    $(selector).after('<div class="form-control text-info font-weight-bold"></div>');
    $(selector).next('.form-control').daterangepicker({
        autoApply: true,
        singleDatePicker: true,
        startDate: momentDate ? momentDate : moment(),
        locale: {
            format: 'DD.MM.YYYY HH:mm'
        },
    }, function (pickerStart, pickerEnd, label) {
        $(selector).next('.form-control').html(pickerStart.format("DD.MM.YYYY"));
    });
    $(selector).next('.form-control').html(momentDate.format("DD.MM.YYYY"));
}

function createDateTimePicker(selector, momentDate) {
    $(selector).next('.form-control').remove();
    $(selector).addClass('d-none');
    $(selector).after('<div class="form-control text-info font-weight-bold"></div>');
    $(selector).next('.form-control').daterangepicker({
        singleDatePicker: true,
        timePicker: true,
        timePicker24Hour: true,
        startDate: momentDate ? momentDate : moment(),
        locale: {
            format: 'DD.MM.YYYY HH:mm',
            applyLabel: 'Uygula',
            cancelLabel: "İptal"
        },
    }, function (pickerStart, pickerEnd, label) {
        $(selector).next('.form-control').html(pickerStart.format("DD.MM.YYYY HH:mm"));
    });
    $(selector).next('.form-control').html(momentDate.format("DD.MM.YYYY HH:mm"));
}

$(document).ready(function () {
    if (document.URL.toLowerCase().indexOf("randevu") > -1) {
        $('input[name=selectPatient]').selectize({
            valueField: 'TCNo',
            labelField: 'NameSurname',
            searchField: ['NameSurname', 'TCNo'],
            create: false,
            maxItems: 1,
            render: {
                option: function (item, escape) {
                    var birthDateUnix = item.BirthDate.match(/([0-9]{1,})/g);
                    return '<div class="media">' +
                        '<img class="mr-3" style="width:64px;" src="/Content/images/' + (item.Gender == 'Bay' ? 'male.png' : 'female.png') + '" alt="' + escape(item.NameSurname) + '">' +
                        '<div class="media-body">' +
                        '<h5 class="mt-0">' + escape(item.NameSurname) + '</h5>' +
                        '<span class="mr-1">' + escape(item.TCNo) + '</span></span><span class="description">' + escape(moment(birthDateUnix * 1).format("DD MMMM YYYY")) + '</span></div></div>';
                },
                item: function (item, escape) {
                    return '<div class="d-inline-block"><div class="media" style="display:flex;">' +
                        '<img class="mr-3" style="width:32px;" src="/Content/images/' + (item.Gender == 'Bay' ? 'male.png' : 'female.png') + '" alt="' + escape(item.NameSurname) + '">' +
                        '<div class="media-body">' +
                        '<h5 class="m-0">' + escape(item.NameSurname) + '</h5></div></div></div>';
                }
            },
            load: function (query, callback) {
                if (!query.length) return callback();

                GetPatients("", function (data) {
                    callback(data);
                });
            }
        });
        var randevuEkle = $('#randevuEkle');
        var randevuSil = $('#randevuSil');
        var scheduler = $('#calendar').fullCalendar({
            themeSystem: 'bootstrap4',
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            allDayDefault: false,
            defaultDate: moment(),
            navLinks: true,
            selectable: true,
            selectHelper: true,
            select: function (start, end) {
                createDateTimePicker('#randevuZamani', start);
                randevuEkle.modal('show');
            },
            editable: true,
            timeFormat: 'HH:mm',
            eventLimit: true,
            events: function (start, end, timezone, callback) {
                GetAppointments(null, function (data) {
                    var events = [];
                    $.each(data, function () {
                        var appointmentDateUnix = this.AppointmentDate.match(/([0-9]{1,})/g);
                        events.push({
                            id: this.ID,
                            patientId: this.TCNo,
                            title: this.NameSurname,
                            start: moment(appointmentDateUnix * 1),
                            end: moment(appointmentDateUnix * 1).add(30, 'minutes'),
                            backgroundColor: (moment(appointmentDateUnix * 1).fromNow().indexOf('önce') > -1 ? '' : '#f96332'),
                            color: (moment(appointmentDateUnix * 1).fromNow().indexOf('önce') > -1 ? '' : '#f96332'),
                            textColor: '#fff'
                        });
                    });
                    callback(events);
                });
            },
            eventClick: function (calEvent, jsEvent, view) {
                randevuSil.find('input[name=randevuID]').val(calEvent.id);
                randevuSil.find('strong#randevuHastaAdi').html(calEvent.title);
                randevuSil.find('strong#randevuTarihSaat').html(calEvent.start.format("DD MMMM YYYY, HH:mm"));
                randevuSil.modal('show');
            }
        });
        $(document).on('click', '#randevuKaydet', function () {
            var thisBtn = $(this);
            var thisBtnContainer = thisBtn.val();
            thisBtn.html('<i class="fa fa-fw fa-spin fa-circle-o-notch"></i> Hasta Kaydediliyor. Lütfen Bekleyin...');
            if ($('[name=userType]').val() === 'existing') {
                var eventData = {
                    title: $(this).parents('.modal').find('input[name=selectPatient]').val(),
                    start: moment($(this).parents('.modal').find('input#randevuZamani').next('.form-control').html(), 'DD.MM.YYYY HH:mm')
                };
            } else {
                var NameSurname = $('input[name=txtPatient]').val().split(' ');
                var name = NameSurname[0];

                for (var i = 1; i < NameSurname.length - 1; i++) {
                    name += ' ' + NameSurname[i];
                }

                AddPatient($('input[name=TCNo]').val(), '', name, NameSurname[NameSurname.length - 1],
                    $('input[name=birthDate]').next('.form-control').html(), '', '', 0, $('select[name=selectGender] option:selected').val(),
                    4, 37, function () {
                        thisBtn.html('<i class="fa fa-fw fa-spin fa-circle-o-notch"></i> Randevu Kaydediliyor. Lütfen Bekleyin...');
                    }, true);
                var eventData = {
                    title: '',
                    start: moment($(this).parents('.modal').find('input#randevuZamani').next('.form-control').html(), 'DD.MM.YYYY HH:mm')
                };
            }
            AddAppointment(eventData.title, eventData.start.format("YYYY-MM-DD HH:mm"), function (data) {
                randevuEkle.modal('hide');
                thisBtn.html(thisBtnContainer);
                $('#calendar').fullCalendar('refetchEvents');
                if (data) {
                    showNotification('success', 'Randevu Başarıyla Kaydedildi!');
                } else {
                    show('danger', 'Randevu kaydedilirken bir hata oluştu! Tekrar deneyin veya sistem yöneticinizle görüşün.');
                }
            });
        });
        $(document).on('click', '#btnRandevuSil', function () {
            var thisBtn = $(this);
            var thisBtnContainer = thisBtn.val();
            thisBtn.html('<i class="fa fa-fw fa-spin fa-circle-o-notch"></i> İşlem Yapılıyor. Lütfen Bekleyin...');
            DeleteAppointment(randevuSil.find('input[name=randevuID]').val(), function (data) {
                randevuSil.modal('hide');
                thisBtn.html(thisBtnContainer);
                $('#calendar').fullCalendar('refetchEvents');
                if (data) {
                    showNotification('success', 'Randevu Başarıyla Silindi!');
                } else {
                    show('danger', 'Randevu silinirken bir hata oluştu! Tekrar deneyin veya sistem yöneticinizle görüşün.');
                }
            });
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

        createDatePicker("[name=birthDate]", moment().subtract(5, 'years'));

        randevuEkle.on('show.bs.modal', function (e) {
            $('a#existingPatient').trigger('click');
        });
    }
    if ($("#hastaListesi").length > 0) {
        GetPatients(null, function (data) {
            $.each(data, function () {
                var birthDateUnix = this.BirthDate.match(/([0-9]{1,})/g);
                this.BirthDate = moment(birthDateUnix * 1).format('DD MMMM YYYY');
            });
            $("#hastaListesi").html('');
            $.template("patientTemplate", $('#patientTemplate').html());
            $.tmpl("patientTemplate", data).appendTo("#hastaListesi");
        });
        $(document).on('input', '#hastaAra', function () {
            var qText = $(this);
            var qTextVal = qText.val();
            GetPatients(qTextVal, function (data) {
                if (qTextVal == qText.val()) {
                    $.each(data, function () {
                        var birthDateUnix = this.BirthDate.match(/([0-9]{1,})/g);
                        this.BirthDate = moment(birthDateUnix * 1).format('DD MMMM YYYY');
                    });
                    $("#hastaListesi").html('');
                    $.template("patientTemplate", $('#patientTemplate').html());
                    $.tmpl("patientTemplate", data).appendTo("#hastaListesi");
                }
            });
        });
        $(document).on('click', '.btnHastaKaldir', function () {
            var thisData = $(this).data('id');
            DeletePatient(thisData, function (data) {
                if (data) {
                    showNotification('success', 'Hasta Başarıyla Silindi!');
                    GetPatients($('#hastaAra').val(), function (pData) {
                        $.each(pData, function () {
                            var birthDateUnix = this.BirthDate.match(/([0-9]{1,})/g);
                            this.BirthDate = moment(birthDateUnix * 1).format('DD MMMM YYYY');
                        });
                        $("#hastaListesi").html('');
                        $.template("patientTemplate", $('#patientTemplate').html());
                        $.tmpl("patientTemplate", pData).appendTo("#hastaListesi");
                    });
                } else {
                    showNotification('success', 'Hasta silinirken bir hata ile karşılaştık! Tekrar deneyin veya sistem yönetiniz ile görüşün.');
                }
            });
        });
    }
    if ($("#giderListesi").length > 0) {
        GetExpenses(null, function (data) {
            $.each(data, function () {
                var PaymentDateUnix = this.PaymentDate.match(/([0-9]{1,})/g);
                this.PaymentDate = moment(PaymentDateUnix * 1).format('DD MMMM YYYY');
            });
            $("#giderListesi").html('');
            $.template("expenseTemplate", $('#expenseTemplate').html());
            $.tmpl("expenseTemplate", data).appendTo("#giderListesi");
        });
        $(document).on('input', '#giderAra', function () {
            var qText = $(this);
            var qTextVal = qText.val();
            GetExpenses(qTextVal, function (data) {
                if (qTextVal == qText.val()) {
                    $.each(data, function () {
                        var PaymentDateUnix = this.PaymentDate.match(/([0-9]{1,})/g);
                        this.PaymentDate = moment(PaymentDateUnix * 1).format('DD MMMM YYYY');
                    });
                    $("#giderListesi").html('');
                    $.template("expenseTemplate", $('#expenseTemplate').html());
                    $.tmpl("expenseTemplate", data).appendTo("#giderListesi");
                }
            });
        });
        $(document).on('click', '.btnGiderKaldir', function () {
            DeleteExpenses($(this).data('id'), function (data) {
                if (data) {
                    showNotification('success', 'Gider kaldırma işlemi başarıyla gerçekleştirildi.');
                } else {
                    showNotification('danger', 'Gider kaldırma işlemi gerçekleştirilirken bir hata ile karşılaştık. Tekrar deneyin veya sistem yöneticisi ile görüşün.');
                }
                GetExpenses($('#giderAra').val(), function (data) {
                    $.each(data, function () {
                        var PaymentDateUnix = this.PaymentDate.match(/([0-9]{1,})/g);
                        this.PaymentDate = moment(PaymentDateUnix * 1).format('DD MMMM YYYY');
                    });
                    $("#giderListesi").html('');
                    $.template("expenseTemplate", $('#expenseTemplate').html());
                    $.tmpl("expenseTemplate", data).appendTo("#giderListesi");
                });
            });
        });
    }
    if ($("#stokListesi").length > 0) {
        GetStocks(null, function (data) {
            $("#stokListesi").html('');
            $.template("stockTemplate", $('#stockTemplate').html());
            $.tmpl("stockTemplate", data).appendTo("#stokListesi");
        });
        $(document).on('input', '#stokAra', function () {
            var qText = $(this);
            var qTextVal = qText.val();
            GetStocks(qTextVal, function (data) {
                if (qTextVal == qText.val()) {
                    $("#stokListesi").html('');
                    $.template("stockTemplate", $('#stockTemplate').html());
                    $.tmpl("stockTemplate", data).appendTo("#stokListesi");
                }
            });
        });
        $(document).on('click', '.btnGiderKaldir', function () {
            DeleteExpenses($(this).data('id'), function (data) {
                if (data) {
                    showNotification('success', 'Gider kaldırma işlemi başarıyla gerçekleştirildi.');
                } else {
                    showNotification('danger', 'Gider kaldırma işlemi gerçekleştirilirken bir hata ile karşılaştık. Tekrar deneyin veya sistem yöneticisi ile görüşün.');
                }
                GetExpenses($('#giderAra').val(), function (data) {
                    $.each(data, function () {
                        var PaymentDateUnix = this.PaymentDate.match(/([0-9]{1,})/g);
                        this.PaymentDate = moment(PaymentDateUnix * 1).format('DD MMMM YYYY');
                    });
                    $("#giderListesi").html('');
                    $.template("expenseTemplate", $('#expenseTemplate').html());
                    $.tmpl("expenseTemplate", data).appendTo("#giderListesi");
                });
            });
        });
    }
    if ($("#tedarikciListesi").length > 0) {
        GetSuppliers(null, function (data) {
            $("#tedarikciListesi").html('');
            $.template("supplierTemplate", $('#supplierTemplate').html());
            $.tmpl("supplierTemplate", data).appendTo("#tedarikciListesi");
        });
        $(document).on('input', '#tedarikciAra', function () {
            var qText = $(this);
            var qTextVal = qText.val();
            GetSuppliers(qTextVal, function (data) {
                if (qText.val() == qTextVal) {
                    $("#tedarikciListesi").html('');
                    $.template("supplierTemplate", $('#supplierTemplate').html());
                    $.tmpl("supplierTemplate", data).appendTo("#tedarikciListesi");
                }
            });
        });
        $(document).on('click', '.btnTedarikciKaldir', function () {
            DeleteSupplier($(this).data('id'), function (data) {
                if (data) {
                    showNotification('success', 'Gider kaldırma işlemi başarıyla gerçekleştirildi.');
                } else {
                    showNotification('danger', 'Gider kaldırma işlemi gerçekleştirilirken bir hata ile karşılaştık. Tekrar deneyin veya sistem yöneticisi ile görüşün.');
                }
                GetSuppliers($('#tedarikciAra').val(), function (data) {
                    $("#tedarikciListesi").html('');
                    $.template("supplierTemplate", $('#supplierTemplate').html());
                    $.tmpl("supplierTemplate", data).appendTo("#tedarikciListesi");
                });
            });
        });
    }

    if (document.URL.toLowerCase().indexOf("hasta/ekle") > -1) {
        GetBloodGroups(function (data) {
            data.forEach(function (blood) {
                $('#kanGrubu').append('<option value="' + blood.ID + '">' + blood.BloudGroupName + '</option>');
            });

            GetCurrencies(function (data) {
                data.forEach(function (currency) {
                    $('#paraBirimi').append('<option value="' + currency.ID + '">' + currency.CurrencyName + '</option>');
                });
                GetCountries(function (data) {
                    data.forEach(function (country) {
                        $('#uyruk').append('<option value="' + country.ID + '">' + country.CountryName + '</option>');
                    });
                    $('.card-body>.loading').remove();
                });
            });
        });
        $(document).on('click', '#kaydetHasta:not(.disabled)', function () {
            var thisBtn = $(this);
            var btnText = thisBtn.html();
            thisBtn.addClass('disabled');
            thisBtn.html('<i class="fa fa-fw fa-spin fa-circle-o-notch"></i> İşlem Yapılıyor. Lütfen Bekleyin...');
            var gender = 'E';
            if ($('#kadin').is(':checked')) {
                gender = 'K';
            }
            AddPatient(
                $('#TCNo').val(),
                $('#ePosta').val(),
                $('#ad').val(),
                $('#soyad').val(),
                $('#dogumTarihi').val(),
                $('#cepTelefonu').val(),
                $('#adres').val(),
                $('#kanGrubu option:selected').val(), gender,
                $('#paraBirimi option:selected').val(),
                $('#uyruk option:selected').val(), function (data) {
                    thisBtn.html(btnText);
                    thisBtn.removeClass('disabled');
                    if (data)
                        showNotification('success', "Hasta kaydınız başarıyla tamamlanmıştır!");
                    else
                        showNotification('danger', "Hasta kaydınız oluşturulurken bir hatayla karşılaşıldı. Sistem yöneticiniz ile görüşün.");
                });
        });
    }
    var hastaGuncelle = document.URL.toLowerCase().match(/dashboard\/hasta\/([0-9]{1,})/g);
    if (hastaGuncelle) {
        var urlArray = (hastaGuncelle + '').split('/');
        var id = urlArray[2];

        $('#btnTedaviler').removeClass('d-none');

        GetBloodGroups(function (data) {
            data.forEach(function (blood) {
                $('#kanGrubu').append('<option value="' + blood.ID + '">' + blood.BloudGroupName + '</option>');
            });

            GetCurrencies(function (data) {
                data.forEach(function (currency) {
                    $('#paraBirimi').append('<option value="' + currency.ID + '">' + currency.CurrencyName + '</option>');
                });
                GetCountries(function (data) {
                    data.forEach(function (country) {
                        $('#uyruk').append('<option value="' + country.ID + '">' + country.CountryName + '</option>');
                    });
                    GetPatient(id, function (data) {
                        var NameSurname = data.NameSurname.split(' ');
                        var name = NameSurname[0];

                        for (var i = 1; i < NameSurname.length - 1; i++) {
                            name += ' ' + NameSurname[i];
                        }

                        var birthDateUnix = data.BirthDate.match(/([0-9]{1,})/g);
                        data.BirthDate = moment(birthDateUnix * 1).format('YYYY-MM-DD');

                        $('#TCNo').val(data.TCNo);
                        $('#ePosta').val(data.Email);
                        $('#ad').val(name);
                        $('#soyad').val(NameSurname[NameSurname.length - 1]);
                        $('#dogumTarihi').val(data.BirthDate);
                        $('#cepTelefonu').val(data.Telephone);
                        $('#adres').val(data.Address);

                        $('#btnTedaviler').attr('href', '/Dashboard/Tedavi/' + data.TCNo);

                        if (data.Gender == "Bay") {
                            $('#erkek').attr("checked", "checked");
                        }

                        $('#kanGrubu option').each(function () {
                            if (data.BloodGroup == $(this).html()) {
                                $(this).attr("selected", "selected");
                                return false;;
                            }
                        });
                        $('#paraBirimi option').each(function () {
                            if (data.Currency == $(this).html()) {
                                $(this).attr("selected", "selected");
                                return false;;
                            }
                        });
                        $('#uyruk option').each(function () {
                            if (data.Country == $(this).html()) {
                                $(this).attr("selected", "selected");
                                return false;;
                            }
                        });
                        $('.card-body>.loading').remove();
                    });
                });
            });
        });
        $('#TCNo').attr("disabled", "disabled");

        $(document).on('click', '#kaydetHasta:not(.disabled)', function () {
            var thisBtn = $(this);
            var btnText = thisBtn.html();
            thisBtn.addClass('disabled');
            thisBtn.html('<i class="fa fa-fw fa-spin fa-circle-o-notch"></i> İşlem Yapılıyor. Lütfen Bekleyin...');
            var gender = 'E';
            if ($('#kadin').is(':checked')) {
                gender = 'K';
            }
            UpdatePatient(
                id, $('#ePosta').val(),
                $('#ad').val(),
                $('#soyad').val(),
                $('#dogumTarihi').val(),
                $('#cepTelefonu').val(),
                $('#adres').val(),
                $('#kanGrubu option:selected').val(), gender,
                $('#paraBirimi option:selected').val(),
                $('#uyruk option:selected').val(), function (data) {
                    thisBtn.html(btnText);
                    thisBtn.removeClass('disabled');
                    if (data)
                        showNotification('success', "Hasta kaydınız başarıyla güncellenmiştir!");
                    else
                        showNotification('danger', "Hasta kaydınız güncellenirken bir hatayla karşılaşıldı. Tekrar deneyiniz veya sistem yöneticiniz ile görüşün.");
                });
        });
    }

    if (document.URL.toLowerCase().indexOf("gider/ekle") > -1) {
        GetExpenseTypes(function (data) {
            data.forEach(function (expenseType) {
                $('#giderTuru').append('<option value="' + expenseType.ID + '">' + expenseType.ExpenseName + '</option>');
            });
            $('.card-body>.loading').remove();
        });
        $(document).on('click', '#giderKaydet:not(.disabled)', function () {
            var thisBtn = $(this);
            var btnText = thisBtn.html();
            thisBtn.addClass('disabled');
            thisBtn.html('<i class="fa fa-fw fa-spin fa-circle-o-notch"></i> İşlem Yapılıyor. Lütfen Bekleyin...');
            AddExpenses($('#giderTuru option:selected').val(), $('#aciklama').val(), $('#odenenTutar').val(), $('#odemeTarihi').val(), function (data) {
                thisBtn.html(btnText);
                thisBtn.removeClass('disabled');
                if (data) {
                    showNotification('success', 'Gider ekleme işlemi başarıyla gerçekleşti!');
                } else {
                    showNotification('danger', 'Gider ekleme işlemi gerçekleştirilirken bir hata oluştu! İşleminizi kontrol ederek tekrar deneyin veya sistem yöneticiniz ile görüşün.');
                }
            });
        });
    }
    var giderGuncelle = document.URL.toLowerCase().match(/dashboard\/gider\/([0-9]{1,})/g);
    if (giderGuncelle) {
        var urlArray = (giderGuncelle + '').split('/');
        var id = urlArray[2];

        GetExpenseTypes(function (data) {
            data.forEach(function (expenseType) {
                $('#giderTuru').append('<option value="' + expenseType.ID + '">' + expenseType.ExpenseName + '</option>');
            });
            GetExpense(id, function (data) {
                $('#giderTuru option').each(function () {
                    if (data.ExpenseName == $(this).html()) {
                        $(this).attr('selected', 'selected');
                        return false;
                    }
                });

                var paymentDateUnix = data.PaymentDate.match(/([0-9]{1,})/g);
                data.PaymentDate = moment(paymentDateUnix * 1).format('YYYY-MM-DD');

                $('#odemeTarihi').val(data.PaymentDate);
                $('#odenenTutar').val(data.Payment);
                $('#aciklama').val(data.ExpenseDescription);
                $('.card-body>.loading').remove();
            });
        });

        $(document).on('click', '#giderKaydet:not(.disabled)', function () {
            var thisBtn = $(this);
            var btnText = thisBtn.html();
            thisBtn.addClass('disabled');
            thisBtn.html('<i class="fa fa-fw fa-spin fa-circle-o-notch"></i> İşlem Yapılıyor. Lütfen Bekleyin...');
            UpdateExpenses(id, $('#giderTuru option:selected').val(), $('#aciklama').val(), $('#odenenTutar').val(), $('#odemeTarihi').val(), function (data) {
                thisBtn.html(btnText);
                thisBtn.removeClass('disabled');
                if (data) {
                    showNotification('success', 'Gider güncelleme işlemi başarıyla gerçekleşti!');
                } else {
                    showNotification('danger', 'Gider güncelleme işlemi gerçekleştirilirken bir hata oluştu! İşleminizi kontrol ederek tekrar deneyin veya sistem yöneticiniz ile görüşün.');
                }
            });
        });
    }

    if (document.URL.toLowerCase().indexOf("stok/ekle") > -1) {
        GetSupplierMaterials('', function (data) {
            data.forEach(function (material) {
                $('[name=tedarikci]').append('<option value="' + material.ID + '">' + material.MaterialName + '</option>');
            });
            $('.card-body>.loading').remove();
        });

        $(document).on('click', '#btnMalzemeKaydet:not(.disabled)', function () {
            var thisBtn = $(this);
            var btnText = thisBtn.html();
            thisBtn.addClass('disabled');
            thisBtn.html('<i class="fa fa-fw fa-spin fa-circle-o-notch"></i> İşlem Yapılıyor. Lütfen Bekleyin...');
            AddStock($('[name=tedarikci] option:selected').val(), $('[name=stokAdedi]').val(), function (data) {
                thisBtn.html(btnText);
                thisBtn.removeClass('disabled');
                if (data) {
                    showNotification('success', 'Stok ekleme işlemi başarıyla gerçekleşti!');
                } else {
                    showNotification('danger', 'Stok ekleme işlemi gerçekleştirilirken bir hata oluştu! İşleminizi kontrol ederek tekrar deneyin veya sistem yöneticiniz ile görüşün.');
                }
            });
        });
    }

    var stokGuncelle = document.URL.toLowerCase().match(/dashboard\/stok\/([0-9]{1,})/g);
    if (stokGuncelle) {
        var urlArray = (stokGuncelle + '').split('/');
        var id = urlArray[2];

        GetSupplierMaterials('', function (data) {
            data.forEach(function (material) {
                $('[name=tedarikci]').append('<option value="' + material.ID + '">' + material.MaterialName + '</option>');
            });
            GetStock(id, function (data) {
                $('[name=tedarikci] option').each(function () {
                    if (data.MaterialID == $(this).val()) {
                        $(this).attr('selected', 'selected');
                        return false;
                    }
                });

                $('[name=stokAdedi]').val(data.Quantity);

                $('.card-body>.loading').remove();
            });
        });

        $(document).on('click', '#btnMalzemeKaydet:not(.disabled)', function () {
            var thisBtn = $(this);
            var btnText = thisBtn.html();
            thisBtn.addClass('disabled');
            thisBtn.html('<i class="fa fa-fw fa-spin fa-circle-o-notch"></i> İşlem Yapılıyor. Lütfen Bekleyin...');
            AddStock($('[name=tedarikci] option:selected').val(), $('[name=stokAdedi]').val(), function (data) {
                thisBtn.html(btnText);
                thisBtn.removeClass('disabled');
                if (data) {
                    showNotification('success', 'Stok güncelleme işlemi başarıyla gerçekleşti!');
                } else {
                    showNotification('danger', 'Stok güncelleme işlemi gerçekleştirilirken bir hata oluştu! İşleminizi kontrol ederek tekrar deneyin veya sistem yöneticiniz ile görüşün.');
                }
            });
        });
    }

    if (document.URL.toLowerCase().indexOf("tedarikci/ekle") > -1) {
        $(document).on('click', '#tedarikciKaydet:not(.disabled)', function () {
            var thisBtn = $(this);
            var btnText = thisBtn.html();
            thisBtn.addClass('disabled');
            thisBtn.html('<i class="fa fa-fw fa-spin fa-circle-o-notch"></i> İşlem Yapılıyor. Lütfen Bekleyin...');
            AddSupplier($('#fad').val(), $('#address').val(), $('#cep').val(), $('#email').val(), function (data) {
                thisBtn.html(btnText);
                thisBtn.removeClass('disabled');
                if (data) {
                    showNotification('success', 'Tedarikçi ekleme işlemi başarıyla gerçekleşti!');
                } else {
                    showNotification('danger', 'Tedarikci ekleme işlemi gerçekleştirilirken bir hata oluştu! İşleminizi kontrol ederek tekrar deneyin veya sistem yöneticiniz ile görüşün.');
                }
            });
        });
    }

    var tedarikciGuncelle = document.URL.toLowerCase().match(/dashboard\/tedarikci\/([0-9]{1,})/g);
    if (tedarikciGuncelle) {
        var urlArray = (tedarikciGuncelle + '').split('/');
        var id = urlArray[2];

        GetSupplier(id, function (data) {
            $('#fad').val(data.SupplierName);
            $('#cep').val(data.Telephone);
            $('#email').val(data.Email);
            $('#address').val(data.Address);

            $('.card-body>.loading').remove();
        });

        $(document).on('click', '#tedarikciKaydet:not(.disabled)', function () {
            var thisBtn = $(this);
            var btnText = thisBtn.html();
            thisBtn.addClass('disabled');
            thisBtn.html('<i class="fa fa-fw fa-spin fa-circle-o-notch"></i> İşlem Yapılıyor. Lütfen Bekleyin...');
            UpdateSupplier(id, $('#fad').val(), $('#address').val(), $('#cep').val(), $('#email').val(), function (data) {
                thisBtn.html(btnText);
                thisBtn.removeClass('disabled');
                if (data) {
                    showNotification('success', 'Tedarikçi ekleme işlemi başarıyla gerçekleşti!');
                } else {
                    showNotification('danger', 'Tedarikci ekleme işlemi gerçekleştirilirken bir hata oluştu! İşleminizi kontrol ederek tekrar deneyin veya sistem yöneticiniz ile görüşün.');
                }
            });
        });
    }

    if (document.URL.toLowerCase().indexOf("login") > -1) {
        $(document).on('keypress', '#txtUserName', function (event) {
            if (event.which == 13)
                $('#btnLogin:not(.disabled)').trigger('click');
        });
        $(document).on('keypress', '#txtPassword', function (event) {
            if (event.which == 13)
                $('#btnLogin:not(.disabled)').trigger('click');
        });
        $(document).on('click', '#btnLogin:not(.disabled)', function () {
            var thisBtn = $(this);
            var btnText = thisBtn.html();
            thisBtn.addClass('disabled');
            thisBtn.html('<i class="fa fa-fw fa-spin fa-circle-o-notch"></i> Giriş Yapılıyor...');
            Login($('#txtUserName').val(), $('#txtPassword').val(), function (data) {
                if (data) {
                    window.location.href = '/Dashboard/Randevu';
                } else {
                    thisBtn.html(btnText);
                    thisBtn.removeClass('disabled');
                    showNotification('danger', 'Kullanıcı adı veya şifre yanlış! Lütfen değerleri kontrol ederek tekrar deneyiniz.');
                }
            });
        });
    } else {
        $(document).on('click', '#btnLogout', function () {
            Logout(function (data) {
                if (data) {
                    window.location.href = '/Dashboard/Login';
                } else {
                    showNotification('danger', 'Çıkış yapılırken bir hata oluştu. Tarayıcınızı kapatarak da çıkış yapabilirsiniz.');
                }
            });
        });
    }

    var tedaviGuncelle = document.URL.toLowerCase().match(/dashboard\/tedavi\/([0-9]{1,})/g);
    if (tedaviGuncelle) {
        var urlArray = (tedaviGuncelle + '').split('/');
        var id = urlArray[2];

        var tedaviEkleModal = $('#modalTedaviEkle');

        GetPatient(id, function (data) {
            $('#hastaAdi').html(data.NameSurname);
            $('#hastaAdi').attr('href', '/Dashboard/Hasta/' + id);

            var birthDateUnix = data.BirthDate.match(/([0-9]{1,})/g);
            data.BirthDate = moment(birthDateUnix * 1).format('DD MMMM YYYY');
            $('#hastaDogumTarihi').html(data.BirthDate);

            $('#hastaKanGrubu').html(data.BloodGroup);
            $('#hastaCinsiyet').html(data.Gender);
            $('#hastaAvatar').attr('src', '/Content/images/' + (data.Gender === 'Bay' ? 'male' : 'female') + '.png');

            GetDentists('', function (data) {
                data.forEach(function (dentist) {
                    $('#disHekimi').append('<option value="' + dentist.ID + '">' + dentist.NameSurname + '</option>');
                });
            });
        });

        GetTreatments('', id, function (data) {
            var totalPrice = 0;
            $.each(data, function () {
                var TreatmentTimeUnix = this.TreatmentTime.match(/([0-9]{1,})/g);
                this.TreatmentTime = moment(TreatmentTimeUnix * 1).format('DD MMMM YYYY');

                totalPrice += this.Price;
                this.Price = this.Price.toFixed(2);
            });
            $('#toplamTedaviTutari').html('' + totalPrice.toFixed(2) + ' ₺');

            $("#tedaviListesi").html('');
            $.template("treatmentTemplate", $('#treatmentTemplate').html());
            $.tmpl("treatmentTemplate", data).appendTo("#tedaviListesi");
        });

        $('#tedaviTuru').selectize({
            valueField: 'ID',
            labelField: 'TreatmentTypeName',
            searchField: 'TreatmentTypeName',
            create: false,
            maxItems: 1,
            render: {
                option: function (item, escape) {
                    return '<div class="media"><div class="media-body"><h6 class="my-0">' + escape(item.TreatmentTypeName) + '</h6>' +
                        '<span class="description">' + escape(item.Price.toFixed(2)) + ' ₺</span></div></div>';
                },
                item: function (item, escape) {
                    return '<div class="media"><div class="media-body"><h6 class="my-0">' + escape(item.TreatmentTypeName) + '</h6>' +
                        '<span class="description">' + escape(item.Price.toFixed(2)) + ' ₺</span></div></div>';
                }
            },
            load: function (query, callback) {
                if (!query.length) return callback();

                GetTreatmentTypes("", function (data) {
                    callback(data);
                });
            }
        });

        $(document).on('click', '#disSecimAlani a', function () {
            tedaviEkleModal.find('#disNumarasi').html($(this).parent().find('div:first-of-type').html());
            tedaviEkleModal.modal('show');
        });

        $(document).on('click', '#btnTedaviEkle:not(.disabled)', function () {
            var thisBtn = $(this);
            var btnText = thisBtn.html();
            thisBtn.addClass('disabled');
            thisBtn.html('<i class="fa fa-fw fa-spin fa-circle-o-notch"></i> İşlem Yapılıyor. Lütfen Bekleyin...');
            AddTreatment($('#disHekimi option:selected').val(), id, $('#tedaviTuru').val(), tedaviEkleModal.find('#disNumarasi').html(), $('#tedaviAciklmasi').val(), function (data) {
                if (data) {
                    GetTreatments('', function (data) {
                        var totalPrice = 0;
                        $.each(data, function () {
                            var TreatmentTimeUnix = this.TreatmentTime.match(/([0-9]{1,})/g);
                            this.TreatmentTime = moment(TreatmentTimeUnix * 1).format('DD MMMM YYYY');

                            totalPrice += this.Price;
                            this.Price = this.Price.toFixed(2);
                        });
                        $('#toplamTedaviTutari').html('' + totalPrice.toFixed(2) + ' ₺');

                        $("#tedaviListesi").html('');
                        $.template("treatmentTemplate", $('#treatmentTemplate').html());
                        $.tmpl("treatmentTemplate", data).appendTo("#tedaviListesi");
                    });
                    tedaviEkleModal.modal('hide');
                    $(document).scrollTop($('#tedaviListesi').offset().top);
                    showNotification('success', 'Tedavi başarıyla eklenmiştir.');
                } else {
                    showNotification('danger', 'Tedavi eklenirken bir hata meydana geldi. Girdilerinizi kontrol ederek tekrar deneyin veya sistem yöneticiniz ile görüşün.');

                }
            })
        });
    }
});