function AddAppointment(patientID, appointmentDate, callback) {
    $.ajax({
        url: '/webservices/AddAppointment',
        type: 'POST',
        dataType: 'json',
        data: {
            PatientID: patientID,
            AppointmentDate: appointmentDate
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
        dataType: 'json',
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
        dataType: 'json',
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

function AddExpenses(ExpenseType, ExpenseDescription, Payment, PaymentDate, callback) {
    $.ajax({
        url: '/webservices/AddDentist',
        type: 'POST',
        dataType: 'json',
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

function AddExpenses(ExpenseType, ExpenseDescription, Payment, PaymentDate, callback) {
    $.ajax({
        url: '/webservices/AddDentist',
        type: 'POST',
        dataType: 'json',
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

function AddImage(ImagePath, callback) {
    $.ajax({
        url: '/webservices/AddImage',
        type: 'POST',
        dataType: 'json',
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
        dataType: 'json',
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
        dataType: 'json',
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
        dataType: 'json',
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
        dataType: 'json',
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

function AddPatient(TCNo, Email, Name, Surname, BirthDate, Telephone, Address, BloodGroupID, Gender, CurrencyID, CountryID, callback) {
    $.ajax({
        url: '/webservices/AddPatient',
        type: 'POST',
        dataType: 'json',
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
        dataType: 'json',
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
        dataType: 'json',
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
        dataType: 'json',
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

function AddTreatment(DentistID, PatientID, TreatmentTypeID, TreatmentDescription, TreatmentTime, callback) {
    $.ajax({
        url: '/webservices/AddTreatment',
        type: 'POST',
        dataType: 'json',
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
        dataType: 'json',
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
        dataType: 'json',
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
        dataType: 'json',
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
        dataType: 'json',
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}

function GetCountries(callback) {
    $.ajax({
        url: '/webservices/GetBloodGroups',
        type: 'GET',
        dataType: 'json',
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}

function GetCurrencies(callback) {
    $.ajax({
        url: '/webservices/GetBloodGroups',
        type: 'GET',
        dataType: 'json',
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}

function GetDentists(callback) {
    $.ajax({
        url: '/webservices/GetDentists',
        type: 'GET',
        dataType: 'json',
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
        dataType: 'json',
        data: { plainText: plainText },
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
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}

function GetImages(callback) {
    $.ajax({
        url: '/webservices/GetImages',
        type: 'GET',
        dataType: 'json',
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
        dataType: 'json',
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
        dataType: 'json',
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
        dataType: 'json',
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
        dataType: 'json',
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}

function GetStocks(plainText, callback) {
    $.ajax({
        url: '/webservices/GetStocks',
        type: 'GET',
        dataType: 'json',
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}

function GetSupplierMaterials(callback) {
    $.ajax({
        url: '/webservices/GetSupplierMaterials',
        type: 'GET',
        dataType: 'json',
        data: { plainText: plainText },
        success: function (data) {
            callback(data);
        }
    });
}