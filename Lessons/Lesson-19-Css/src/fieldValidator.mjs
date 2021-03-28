function RequiredValidator(element) {
    if (!element) {
        return true;
    }
    switch(element.getAttribute("type")) {
        case "checkbox":
            return element.checked;
        default:
            !!element.value;
    }
}
RequiredValidator.errorText = "is required";

function RegExpValidator(element, regExp) {
    return !element || !element.value || regExp.test(element.value);
}

class FieldValidator {
    static Required = RequiredValidator;

    static RegExp(regExp) {
        var regExpObject = regExp instanceof RegExp ? regExp : new RegExp(regExp);
        var validate = (elem) => RegExpValidator(elem, regExpObject);
        validate.errorText = "should be " + regExp;
        return validate;
    }

    constructor(id, validators) {
        this._id = id;
        this._validators = validators;
        this._isValid = true;
    }
    
    validate() {
        this._clearErrors();
        if (this._validators) {
            this._isValid = true;
            for (const validate of this._validators) {
                if (!validate(this._element)) {
                    this._showError(this._element.name + " " + validate.errorText);
                    this._isValid = false;
                }
            }
        }
    }

    isValid() {
        return this._isValid;
    }

    _clearErrors() {
        for (const elem of this._element.parentElement.children) {
            if (elem.className == 'error') {
                elem.remove();
            }
        }
    }

    _showError(error) {
        var errorElem = document.createElement("span");
        errorElem.className = "error";
        errorElem.innerText = error;
        this._element.parentElement.appendChild(errorElem);
    }
}

export {FieldValidator}