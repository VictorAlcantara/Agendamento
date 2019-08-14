import React, { Component } from 'react';
import { Modal, Button, Row, Col, Form, Alert } from 'react-bootstrap';

export class AddEditSalaModal extends Component {
    constructor(props) {
        super(props);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.state = { errors: [], success: false };
    }

    handleSubmit(event) {
        fetch("http://localhost:4655/api/v1/sala", {
            method: this.props.isAdd ? "POST" : "PUT",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                id: this.props.isAdd ? null : event.target.id.value,
                descricao: event.target.descricao.value
            })
        })
            .then(response => {
                if (!response.ok) {
                    response.json().then((err) => this.setState({ errors: err.errors, success: false }));
                }
                else {
                    this.setState({ errors: [], success: true });
                    response.json();
                }
            }).then(response => {
                this.props.onChangeHandler();
            });

        event.preventDefault();
    }

    onHide() {
    }

    render() {
        const { isAdd, ...rest } = this.props;

        let formGroupId;
        let acaoNome;
        let descricao = "";
        let alert;

        if (!isAdd) {
            formGroupId = <Form.Group controlId="id">
                <Form.Label>Id</Form.Label>
                <Form.Control type="text" name="id" required disabled defaultValue={this.props.id} />
            </Form.Group>;
            acaoNome = "Editar ";
            descricao = this.props.descricao;
        }
        else {
            acaoNome = "Adicionar"
        }

        if (this.state.errors.length > 0) {
            alert =
                <Alert variant="danger">
                    <ul>
                        {this.state.errors.map((e, idx) => <li key={idx}>{e.message}</li>)}
                    </ul>
                </Alert>;
        }
        else if (this.state.success)
        {
            alert =
                <Alert variant="success">Salvo com sucesso!</Alert>;
        }

        return (
            <Modal {...this.props} size="lg" aria-labelledby="contained-modal-title-vcenter" centered>
                <Modal.Header closeButton>
                    <Modal.Title id="contained-modal-title-vcenter">
                        {acaoNome} Sala
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div className="container">
                        {alert}

                        <Row>
                            <Col sm={6}>
                                <Form onSubmit={this.handleSubmit}>
                                    {formGroupId}
                                    <Form.Group controlId="descricao">
                                        <Form.Label>Descrição</Form.Label>
                                        <Form.Control type="text" name="descricao" required defaultValue={descricao} />
                                    </Form.Group>
                                    <Form.Group>
                                        <Button variant="primary" type="submit">Salvar</Button>
                                    </Form.Group>
                                </Form>
                            </Col>
                        </Row>
                    </div>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="danger" onClick={this.props.onHide}>Sair</Button>
                </Modal.Footer>
            </Modal>
        );
    }
}