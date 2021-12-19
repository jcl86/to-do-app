import { Aurelia } from 'aurelia-framework';
import { HttpClient, json } from 'aurelia-fetch-client';

export class App {
  private heading: string;
  private todos: { id: number; content: string; done: boolean; }[];
  private todoDescription: string;
  private baseAddress: string;

  constructor() {
    this.heading = 'Todos';
    this.todos = [];
    this.todoDescription = '';
    this.baseAddress = 'https://localhost:5001';

    this.getTodos();
  }

  getTodos() {
    let httpClient = new HttpClient();
    let url = `${this.baseAddress}/todos`;
    httpClient.fetch(url)
      .then(response => response.json())
      .then(data => {
        this.todos = data;
      });
  }

  addTodo() {
    if (this.todoDescription) {

      let dto = {
        content: this.todoDescription
      }

      let httpClient = new HttpClient();
      let url = `${this.baseAddress}/todos`;
      httpClient.fetch(url, {
        method: 'post',
        body: json(dto)
      }).then(response => response.json())
        .then(data => {
          this.todos.push(data);
          this.todoDescription = '';
        });
    }
  }
  
  removeTodo(id: number) {
    let httpClient = new HttpClient();
    let url = `${this.baseAddress}/todos/${id}`;
    httpClient.fetch(url, {
      method: 'delete'
    }).then(response => {
        let todo = this.todos.find(x => x.id == id)
        let index = this.todos.indexOf(todo);
        if (index !== -1) {
          this.todos.splice(index, 1);
        }
      });
  }
}
