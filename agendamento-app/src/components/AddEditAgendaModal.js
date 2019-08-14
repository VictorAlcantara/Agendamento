import React, { Component } from 'react';
import { Modal, Button, Row, Col, Form, Alert } from 'react-bootstrap';

export class AddEditAgendaModal extends Component {
    constructor(props) {
        super(props);
        this.state = { salas: [], errors: [], success: false };

        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount() {
        fetch("http://localhost:4655/api/v1/sala")
            .then(response => response.json())
            .then(data => this.setState({ salas: data }));
    }

    handleSubmit(event) {
        event.preventDefault();

        fetch("http://localhost:4655/api/v1/agenda", {
            method: this.props.isAdd ? "POST" : "PUT",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                id: this.props.isAdd ? null : event.target.id.value,
                titulo: event.target.titulo.value,
                horarioInicio: event.target.horarioInicio.value,
                horarioFim: event.target.horarioFim.value,
                salaId: event.target.sala.value,
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
    }

    render() {
        const { isAdd, agenda, ...rest } = this.props;

        let formGroupId;
        let acaoNome;
        let alert;

        if (!isAdd) {
            formGroupId = <Form.Group controlId="id">
                <Form.Label>Id</Form.Label>
                <Form.Control type="text" name="id" required disabled defaultValue={agenda.id} />
            </Form.Group>;
            acaoNome = "Editar ";
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
        else if (this.state.success) {
            alert =
                <Alert variant="success">Salvo com sucesso!</Alert>;
        }

        return (
            <Modal {...this.props} size="lg" aria-labelledby="contained-modal-title-vcenter" centered>
                <Modal.Header closeButton>
                    <Modal.Title id="contained-modal-title-vcenter">
                        {acaoNome} Agendamento
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div className="container">
                        {alert}

                        <Row>
                            <Col sm={6}>
                                <Form onSubmit={this.handleSubmit}>
                                    {formGroupId}
                                    <Form.Group controlId="titulo">
                                        <Form.Label>Título</Form.Label>
                                        <Form.Control type="text" name="titulo" required defaultValue={agenda.titulo} />
                                    </Form.Group>
                                    <Form.Group controlId="sala">
                                        <Form.Label>Sala</Form.Label>
                                        <Form.Control as="select" required defaultValue={agenda.sala.id}>
                                            {this.state.salas.map(sala => <option key={sala.id} value={sala.id}>{sala.descricao}</option>)}
                                        </Form.Control>
                                    </Form.Group>
                                    <Form.Group controlId="horarioInicio">
                                        <Form.Label>Horário Inicio</Form.Label>
                                        <Form.Control type="date" name="horarioInicio" required defaultValue={agenda.horarioInicio} />
                                    </Form.Group>
                                    <Form.Group controlId="horarioFim">
                                        <Form.Label>Horário Final</Form.Label>
                                        <Form.Control type="date" name="horarioFim" required defaultValue={agenda.horarioFim} />
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
