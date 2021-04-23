import React from "react";
import AppBar from "@material-ui/core/AppBar";
import { Toolbar } from "@material-ui/core";

function Header() {
    return (
        <AppBar position="static" style={{ background: '#0366EE' }}>
            <Toolbar>
                <div>
                    <a href="/tasks/add" className="button">Создать задачу</a>
                </div >
                <div>
                    <a href="/tasks" className="button">Список задач</a>
                </div>
                <div>
                    <a href="/employees/add" className="button">Добавление сотрудника</a>
                </div>
            </Toolbar>
        </AppBar >
    );
}

export default Header;