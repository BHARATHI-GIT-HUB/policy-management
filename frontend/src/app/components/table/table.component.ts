import { Component, Input, OnInit } from '@angular/core';
import { policy } from '../../model';
import { VerticalRightOutlined } from '@ant-design/icons';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrl: './table.component.scss',
})
export class TableComponent implements OnInit {
  searchValue = '';
  headerData: string[] = [];
  listOfData: any[] = [];
  listOfDisplayData: any[] = [];
  visible = false;
  editCache: { [key: string]: { edit: boolean; data: any } } = {};
  @Input()
  tableData: any[] = [];

  @Input()
  header: string[] = [];

  reset(): void {
    this.searchValue = '';
    this.search();
  }

  search(): void {
    this.visible = false;
    this.listOfDisplayData = this.listOfData.filter(
      (item: any) => item.Provider.indexOf(this.searchValue) !== -1
    );
  }

  startEdit(id: string): void {
    this.editCache[id].edit = true;
  }

  cancelEdit(id: string): void {
    const index = this.listOfData.findIndex((item) => item.id === id);
    this.editCache[id] = {
      data: { ...this.listOfData[index] },
      edit: false,
    };
  }

  saveEdit(id: string): void {
    const index = this.listOfData.findIndex((item) => item.id === id);
    Object.assign(this.listOfData[index], this.editCache[id].data);
    this.editCache[id].edit = false;
  }

  updateEditCache(): void {
    this.listOfData.forEach((item) => {
      this.editCache[item.id] = {
        edit: false,
        data: { ...item },
      };
    });
  }

  getObjectValues(obj: any): any[] {
    return Object.values(obj);
  }

  ngOnInit(): void {
    this.updateEditCache();
    console.log('table data ' + this.tableData);
    this.listOfData = this.tableData;
    this.listOfDisplayData = [...this.listOfData];
    this.headerData = this.header;
    this.updateEditCache();
  }
}
