function showNotification(type, plainText) {
    $.notify({
        icon: "now-ui-icons ui-1_bell-53",
        message: plainText

    }, {
            type: type,
            timer: 8000,
            placement: {
                from: 'bottom',
                align: 'right'
            }
        });
}

function AddAppointment(patientID, appointmentDate, callback) {
    $.ajax({
        url: '/webservices/AddAppointment',
        type: 'POST',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: {
            PatientID: patientID,
            AppointmentDate: appointmentDate
        },
        success: function (data) {
            callback(data);
        }
    });
}

function DeleteAppointment(appID, callback) {
    $.ajax({
        url: '/webservices/DeleteAppointment',
        type: 'POST',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: {
            id: appID
        },
        success: function (data) {
            callback(data);
        }
    });
}

function AddAssistant(Name, Surname, Email, Telephone, Address, Salary, callback) {
    $.ajax({
        url: '/webservices/AddAssistant',
        type: 'POST',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: {
            Name: Name,
            Surname: Surname,
            Email: Email,
            Telephone: Telephone,
            Address: Address,
            Salary: Salary
        },
        success: function (data) {
            callback(data);
        }
    });
}

function AddDentist(Name, Surname, Email, Telephone, Address, Salary, PhotoID, callback) {
    $.ajax({
        url: '/webservices/AddDentist',
        type: 'POST',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: {
            Name: Name,
            Surname: Surname,
            Email: Email,
            Telephone: Telephone,
            Address: Address,
            Salary: Salary,
            PhotoID: PhotoID
        },
        success: function (data) {
            callback(data);
        }
    });
    function AddDentist(Name, Surname, Email, Telephone, Address, Salary, PhotoID, callback) {
        $.ajax({
            url: '/webservices/AddDentist',
            type: 'POST',
            dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
            data: {
                Name: Name,
                Surname: Surname,
                Email: Email,
                Telephone: Telephone,
                Address: Address,
                Salary: Salary,
                PhotoID: PhotoID
            },
            success: function (data) {
                callback(data);
            }
        });
    }
}

function UpdateDentist(ID, Name, Surname, Email, Telephone, Address, Salary, PhotoID, callback) {
    $.ajax({
        url: '/webservices/UpdateDentist',
        type: 'POST',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: {
            id: ID,
            Name: Name,
            Surname: Surname,
            Email: Email,
            Telephone: Telephone,
            Address: Address,
            Salary: Salary,
            PhotoID: PhotoID
        },
        success: function (data) {
            callback(data);
        }
    });
}

function AddExpenses(ExpenseType, ExpenseDescription, Payment, PaymentDate, callback) {
    $.ajax({
        url: '/webservices/AddExpenses',
        type: 'POST',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: {
            ExpenseType: ExpenseType,
            ExpenseDescription: ExpenseDescription,
            Payment: Payment,
            PaymentDate: PaymentDate
        },
        success: function (data) {
            callback(data);
        }
    });
}

function UpdateExpenses(ID, ExpenseType, ExpenseDescription, Payment, PaymentDate, callback) {
    $.ajax({
        url: '/webservices/UpdateExpenses',
        type: 'POST',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: {
            id: ID,
            ExpenseType: ExpenseType,
            ExpenseDescription: ExpenseDescription,
            Payment: Payment,
            PaymentDate: PaymentDate
        },
        success: function (data) {
            callback(data);
        }
    });
}

function AddImage(ImagePath, callback) {
    $.ajax({
        url: '/webservices/AddImage',
        type: 'POST',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: {
            ImagePath: ImagePath
        },
        success: function (data) {
            callback(data);
        }
    });
}

function AddMedicine(MedicineName, Dosage, Usage, Description, callback) {
    $.ajax({
        url: '/webservices/AddMedicine',
        type: 'POST',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: {
            MedicineName: MedicineName,
            Dosage: Dosage,
            Usage: Usage,
            Description: Description
        },
        success: function (data) {
            callback(data);
        }
    });
}

function AddPrescriptionMedicine(MedicineID, PrescriptionID, Quantity, callback) {
    $.ajax({
        url: '/webservices/AddPrescriptionMedicine',
        type: 'POST',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: {
            MedicineID: MedicineID,
            PrescriptionID: PrescriptionID,
            Quantity: Quantity
        },
        success: function (data) {
            callback(data);
        }
    });
}

function AddPrescription(TreatmentID, PrescriptionTime, callback) {
    $.ajax({
        url: '/webservices/AddPrescription',
        type: 'POST',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: {
            TreatmentID: TreatmentID,
            PrescriptionTime: PrescriptionTime
        },
        success: function (data) {
            callback(data);
        }
    });
}

function AddOtherEmployee(Name, Surname, Telephone, Address, Salary, callback) {
    $.ajax({
        url: '/webservices/AddOtherEmployee',
        type: 'POST',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: {
            Name: Name,
            Surname: Surname,
            Telephone: Telephone,
            Address: Address,
            Salary: Salary,
        },
        success: function (data) {
            callback(data);
        }
    });
}

function AddPatient(TCNo, Email, Name, Surname, BirthDate, Telephone, Address, BloodGroupID, Gender, CurrencyID, CountryID, callback, isAsync) {
    $.ajax({
        url: '/webservices/AddPatient',
        type: 'POST',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: {
            TCNo: TCNo,
            Email: Email,
            Name: Name,
            Surname: Surname,
            BirthDate: BirthDate,
            Telephone: Telephone,
            Address: Address,
            BloodGroupID: BloodGroupID,
            Gender: Gender,
            CurrencyID: CurrencyID,
            CountryID: CountryID
        },
        success: function (data) {
            callback(data);
        }
    });
}

function UpdatePatient(TCNo, Email, Name, Surname, BirthDate, Telephone, Address, BloodGroupID, Gender, CurrencyID, CountryID, callback) {
    $.ajax({
        url: '/webservices/UpdatePatient',
        type: 'POST',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: {
            TCNo: TCNo,
            Email: Email,
            Name: Name,
            Surname: Surname,
            BirthDate: BirthDate,
            Telephone: Telephone,
            Address: Address,
            BloodGroupID: BloodGroupID,
            Gender: Gender,
            CurrencyID: CurrencyID,
            CountryID: CountryID
        },
        success: function (data) {
            callback(data);
        }
    });
}

function AddStock(MaterailID, Quantity, callback) {
    $.ajax({
        url: '/webservices/AddStock',
        type: 'POST',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: {
            MaterailID: MaterailID,
            Quantity: Quantity
        },
        success: function (data) {
            callback(data);
        }
    });
}

function AddSupplierMaterial(SupplierID, MaterialTypeID, MaterialName, UnitPrice, callback) {
    $.ajax({
        url: '/webservices/AddSupplierMaterial',
        type: 'POST',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: {
            SupplierID: SupplierID,
            MaterialTypeID: MaterialTypeID,
            MaterialName: MaterialName,
            UnitPrice: UnitPrice
        },
        success: function (data) {
            callback(data);
        }
    });
}

function AddSupplier(SupplierName, Address, Telephone, Email, callback) {
    $.ajax({
        url: '/webservices/AddSupplier',
        type: 'POST',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: {
            SupplierName: SupplierName,
            Address: Address,
            Telephone: Telephone,
            Email: Email
        },
        success: function (data) {
            callback(data);
        }
    });
}

function UpdateSupplier(id, SupplierName, Address, Telephone, Email, callback) {
    $.ajax({
        url: '/webservices/UpdateSupplier',
        type: 'POST',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: {
            id: id,
            SupplierName: SupplierName,
            Address: Address,
            Telephone: Telephone,
            Email: Email
        },
        success: function (data) {
            callback(data);
        }
    });
}

function AddTreatment(DentistID, PatientID, TreatmentTypeID, TreatmentDescription, TreatmentTime, callback) {
    $.ajax({
        url: '/webservices/AddTreatment',
        type: 'POST',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: {
            DentistID: DentistID,
            PatientID: PatientID,
            TreatmentTypeID: TreatmentTypeID,
            TreatmentDescription: TreatmentDescription,
            TreatmentTime: TreatmentTime
        },
        success: function (data) {
            callback(data);
        }
    });
}

function AddUser(UserEmail, Password, UserType, CreateDate, callback) {
    $.ajax({
        url: '/webservices/AddTreatment',
        type: 'POST',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: {
            UserEmail: UserEmail,
            Password: Password,
            UserType: UserType,
            CreateDate: CreateDate
        },
        success: function (data) {
            callback(data);
        }
    });
}

function GetAppointments(plainText, callback) {
    $.ajax({
        url: '/webservices/getappointments',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}

function GetAssistants(callback) {
    $.ajax({
        url: '/webservices/GetAssistants',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}

function GetBloodGroups(callback) {
    $.ajax({
        url: '/webservices/GetBloodGroups',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        success: function (data) {
            callback(data);
        }
    });
}

function GetCountries(callback) {
    $.ajax({
        url: '/webservices/GetCountries',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        success: function (data) {
            callback(data);
        }
    });
}

function GetCurrencies(callback) {
    $.ajax({
        url: '/webservices/GetCurrencies',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        success: function (data) {
            callback(data);
        }
    });
}

function GetDentists(callback) {
    $.ajax({
        url: '/webservices/GetDentists',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}

function GetExpenses(plainText, callback) {
    $.ajax({
        url: '/webservices/GetExpenses',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}

function GetExpense(id, callback) {
    $.ajax({
        url: '/webservices/GetExpense',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: { id: id },
        success: function (data) {
            callback(data);
        }
    });
}

function DeleteExpenses(id, callback) {
    $.ajax({
        url: '/webservices/DeleteExpenses',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: { id: id },
        success: function (data) {
            callback(data);
        }
    });
}

function GetExpenseTypes(callback) {
    $.ajax({
        url: '/webservices/GetExpenseTypes',
        type: 'GET',
        dataType: 'json',
        async: typeof isAsync == "undefined" ? false : true,
        success: function (data) {
            callback(data);
        }
    });
}

function GetImages(callback) {
    $.ajax({
        url: '/webservices/GetImages',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}

function GetMaterialTypes(callback) {
    $.ajax({
        url: '/webservices/GetMaterialTypes',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}

function GetMedicines(callback) {
    $.ajax({
        url: '/webservices/GetMedicines',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}

function GetOtherEmployees(callback) {
    $.ajax({
        url: '/webservices/GetOtherEmployees',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}

function GetPatients(plainText, callback) {
    $.ajax({
        url: '/webservices/GetPatients',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}

function GetPatient(patientID, callback) {
    $.ajax({
        url: '/webservices/GetPatient',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: { patientID: patientID },
        success: function (data) {
            callback(data);
        }
    });
}

function DeletePatient(patientID, callback) {
    $.ajax({
        url: '/webservices/DeletePatient',
        type: 'POST',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: { id: patientID },
        success: function (data) {
            callback(data);
        }
    });
}

function GetStocks(plainText, callback) {
    $.ajax({
        url: '/webservices/GetStocks',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}

function GetStock(ID, callback) {
    $.ajax({
        url: '/webservices/GetStock',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: { id: ID },
        success: function (data) {
            callback(data);
        }
    });
}

function GetStocksWithTotal(plainText, callback) {
    $.ajax({
        url: '/webservices/GetStocksWithTotal',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}

function GetSupplierMaterials(plainText, callback) {
    $.ajax({
        url: '/webservices/GetSupplierMaterials',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}

function GetSuppliers(plainText, callback) {
    $.ajax({
        url: '/webservices/GetSuppliers',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}

function GetSupplier(ID, callback) {
    $.ajax({
        url: '/webservices/GetSupplier',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: { id: ID },
        success: function (data) {
            callback(data);
        }
    });
}

function GetTreatments(callback) {
    $.ajax({
        url: '/webservices/GetTreatments',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}

function GetTreatmentTypes(callback) {
    $.ajax({
        url: '/webservices/GetTreatmentTypes',
        type: 'GET',
        dataType: 'json', async: typeof isAsync == "undefined" ? false : true,
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}