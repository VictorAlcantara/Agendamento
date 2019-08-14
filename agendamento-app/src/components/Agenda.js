import React, { Component } from 'react';
import Table from 'react-bootstrap/Table';
import { Button, ButtonToolbar } from 'react-bootstrap';
import { AddEditAgendaModal } from './AddEditAgendaModal';

export class Agenda extends Component {
    constructor(pros) {
        super(pros);
        this.state = { agendas: [], addEditModalShow: false };

        this.refreshList = this.refreshList.bind(this);
        this.deleteAgenda = this.deleteAgenda.bind(this);
    }

    componentDidMount() {
        this.refreshList();
    }

    deleteAgenda(id) {
        fetch("http://localhost:4655/api/v1/agenda/" + id, {
            method: "DELETE",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
        }).then(response => {
            this.refreshList();
        });
    }

    refreshList() {
        fetch("http://localhost:4655/api/v1/agenda")
            .then(response => response.json())
            .then(data => {
                this.setState({ agendas: data });
            });
    }

    render() {
        const { agendas } = this.state;

        const _sala = { id: 0, descricao: "" };

        const _agenda = {
            id: 0,
            titulo: "",
            horarioInicio: new Date(),
            horarioFim: new Date(),
            sala: _sala
        };

        let addEditModalClose = () => this.setState({ addEditModalShow: false });

        return (
            <div>
                <Table className="mt-4" striped bordered hover variant="dark">
                    <thead>
                        <tr>
                            <th>Título</th>
                            <th>Sala</th>
                            <th>Início</th>
                            <th>Fim</th>
                        </tr>
                    </thead>
                    <tbody>
                        {agendas.map(agenda =>
                            <tr key={agenda.id}>
                                <td>{agenda.titulo}</td>
                                <td>{agenda.sala.descricao}</td>
                                <td>{agenda.horarioInicio}</td>
                                <td>{agenda.horarioFim}</td>
                                <td>
                                    <ButtonToolbar>
                                        <Button variant="danger" size="sm" onClick={() => this.deleteAgenda(agenda.id)}>
                                            Excluir
                                        </Button>
                                    </ButtonToolbar>
                                </td>
                            </tr>
                        )}
                    </tbody>
                </Table>
                <ButtonToolbar>
                    <Button variant="primary" onClick={() => this.setState({ addEditModalShow: true, isAdd: true })}>Novo Agendamento</Button>
                </ButtonToolbar>

                <AddEditAgendaModal show={this.state.addEditModalShow} isAdd={this.state.isAdd} onHide={addEditModalClose}
                    agenda={_agenda} onChangeHandler={this.refreshList} />
            </div>
        );
    }
}
