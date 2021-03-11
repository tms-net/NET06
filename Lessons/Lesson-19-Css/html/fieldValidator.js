function RequiredValidator(element) {
    return !!(element && element.value);
}
RequiredValidator.errorText = "is required";

function RegExpValidator(element, regExp) {
    return !element || !element.value || regExp.match(element.value);
}

class FieldValidator {
    static Required = RequiredValidator;

    static RegExp(regExp) {
        var validate = (elem) => RegExpValidator(elem, regExp);
        validate.errorText = "should be " + regExp;
        return validate;
    }

    constructor(id, validators) {
        this._id = id;
        this._validators = validators;
        this._isValid = true;
    }

    initialize(){        
        this._element = document.getElementById(this._id);
        var wrapper = document.createElement("span");
        this._element.replaceWith(wrapper);
        wrapper.appendChild(this._element);
    }

    validate() {
        this._clearErrors();
        if (this._validators) {
            this._isValid = true;
            this._validators.forEach(validate => {
                if (!validate(this._element)) {
                    this._showError(this._element.name + " " + validate.errorText);
                    this._isValid = false;
                }
            })
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