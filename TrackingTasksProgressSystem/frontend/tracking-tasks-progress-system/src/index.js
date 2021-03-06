import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import { BrowserRouter, Route, Switch, Redirect } from "react-router-dom";

import AddTaskForm from './forms/Task/AddTaskForm';
import EditTaskForm from './forms/Task/EditTaskForm';
import ShortTasksTable from "./tables/ShortTasksTable";
import AddEmployeeForm from './forms/Employee/AddEmployeeForm';
import EditEmployeeForm from './forms/Employee/EditEmployeeForm';
import Header from "./main/Header";

ReactDOM.render(
  <BrowserRouter>
    <Header />
    <App>
      <Switch>
        <Route exact path="/">
          <Redirect to="/tasks" />
        </Route>
        <Route exact path='/tasks' component={ShortTasksTable} />
        <Route path='/tasks/add' component={AddTaskForm} />
        <Route path='/task/:id(\d+)' component={EditTaskForm} />
        <Route path='/employees/add' component={AddEmployeeForm} />
        <Route path='/employee/:id(\d+)' component={EditEmployeeForm} />
      </Switch>
    </App>
  </BrowserRouter>,
  document.getElementById('root')
);