import { FieldValidator } from "../html/fieldValidator";

describe("FieldValidatorTests", function() {
  
    beforeEach(function() {
        
    });

    it("should not check empty fields for RegExp validation", function() {
      //arrage
      var validator = new FieldValidator("myId", [FieldValidator.RegExp("\d+")]);

      //act
      validator.validate();

      //assert
      expect(validator.isValid()).toBe(true);
    });

    it("should correctly check checkbox input field for Required validation", function() {
    });

    it("should correctly check text input field for Required validation", function() {
    });
    
    it("should check all provie validators", function() {
        //arrage
        var validator = new FieldValidator("myId", [() => false, () => true]);
  
        //act
        validator.validate();
  
        //assert
        //check that all validatino functions were called
    });
});