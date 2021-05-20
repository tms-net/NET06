import React, { Component } from 'react';
import { Button, Form, FormGroup, Label, Input, FormText, Col, Badge } from 'reactstrap';

export class CreateListenerTask extends Component {
    constructor(props) {
        super(props);
        this.state = { rules: [] };
    }

    handleRulesInput(evt) {        
        if (evt.charCode == 13) {
            var rules = this.state.rules;
            rules.push(evt.target.value);
            this.setState({ rules: rules });
            evt.target.value = '';
        }
    }

    deleteRule(index) {
        var rules = this.state.rules;
        rules.splice(index, 1);
        this.setState({ rules: rules });
    }

    render() {
        return (
            <div>
                <h1>Create Listener Task</h1>
                <Form>
                    <FormGroup>
                        <Label for="taskName" sm="2">Name</Label>
                        <Col sm={6}>
                            <Input name="taskName" id="taskName" placeholder="add meaningful name" />
                        </Col>
                    </FormGroup>
                    <FormGroup>
                        <Label for="filterRule" sm="2">Filter rules</Label>
                        <Col sm={6}>
                            <Input name="filterRule" id="filterRule" placeholder="enter rule and press enter" onKeyPress={(evt) => this.handleRulesInput(evt)} />
                        </Col>
                        <Col sm={6} style={{ marginTop: 10 }}>
                            {this.state.rules.map((rule, index) =>
                                <Badge key={index} color="secondary" style={{ padding: 8 }}>
                                    {rule}
                                    <Badge color="danger" style={{ marginLeft: 8 }}>
                                        <a href="#" onClick={(evt) => { evt.preventDefault(); this.deleteRule(index); } }>X</a>
                                    </Badge>
                                </Badge>
                            )}
                        </Col>
                    </FormGroup>
                </Form>
            </div>
        );
    }
}