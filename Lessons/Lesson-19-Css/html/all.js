(function() {

    // window.FieldValidator = FieldValidator;

})()

function RequiredValidator(element) {
    if (!element) {
        return true;
    }
    
    switch(element.type) {
        case "checkbox": return element.checked;
        default: return !!element.value;
    };
}

RequiredValidator.errorText = "is required";

function RegExpValidator(element, regExp) {
    return !element || !element.value || regExp.match(element.value);
}

class FieldValidator {
    static Required = RequiredValidator;

    static RegExp(regExp) {
        var validator = (elem) => RegExpValidator(elem, regExp);
        validator.errorText = "should be " + regExp;
        return validator;
    }

    constructor(id, validators) {
        this._id = id;
        this._validators = validators;
        this._isValid = true;
    }

    initialize() {
        this._element = document.getElementById(this._id);
        var wrapper = document.createElement("span");
        this._element.replaceWith(wrapper);
        wrapper.appendChild(this._element);
    }

    isValid() {
        return this._isValid;
    }

    validate() {
        this.clearErrors();
        if (this._element && this._validators) {
            this._isValid = true;
            this._validators.forEach(validator => {
                if (!validator(this._element)) {
                    this.showError(this._element.name + " " + validator.errorText);
                    this._isValid = false;
                };
            });
        }
    }

    clearErrors() {
        for (const elem of this._element.parentElement.children) {
            if (elem.className == 'error') {
                elem.remove();
            }
        }
    }
    
    showError(error) {
        var errorElem = document.createElement("span");
        errorElem.className = "error";
        errorElem.innerText = error;
        this._element.parentElement.appendChild(errorElem);
    }
}

//export {FieldValidator};