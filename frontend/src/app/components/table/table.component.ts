import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
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
  isloading: boolean = true;

  @Input()
  tableData: any[] = [];

  @Input()
  header: any[] = [];

  @Input()
  ogData: any[] = [];

  @Input()
  editableColumn: string[] = [];

  @Output() deletePoliy = new EventEmitter<number>();

  @Output() UpdatedPoliy = new EventEmitter<any>();

  reset(): void {
    this.searchValue = '';
    this.search();
  }

  search(): void {
    this.visible = false;
    this.listOfDisplayData = this.listOfData.filter((item: any) => {
      if (item.PlanName != undefined)
        return item.PlanName.indexOf(this.searchValue) !== -1;
      else return item.Name.indexOf(this.searchValue) !== -1;
    });
  }

  startEdit(id: number): void {
    this.editCache[id.toString()].edit = true;
  }
  isEditable(column: string): boolean {
    if (column == 'TimePeriod') return true;
    else if (column == 'CoverageAmount') return true;
    else if (column == 'Frequency') return true;
    else if (column == 'Email') return true;
    else if (column == 'Mobile') return true;
    else if (column == 'Address') return true;
    else return false;
  }

  delete(id: number): void {
    this.deletePoliy.emit(id);
  }

  cancelEdit(id: string): void {
    const index = this.listOfData.findIndex(
      (item) => item.id.toString() === id
    );
    this.editCache[id] = {
      data: { ...this.listOfData[index] },
      edit: false,
    };
  }

  saveEdit(id: string): void {
    const index = this.listOfData.findIndex((item) => item.id.toString() == id);
    Object.assign(this.listOfData[index], this.editCache[id].data);
    this.editCache[id].edit = false;
    this.UpdatedPoliy.emit(this.editCache[id].data);
    setTimeout(() => {
      window.location.reload();
    }, 500);
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
  getObjectKey(obj: any): any[] {
    return Object.keys(obj);
  }

  ngOnInit(): void {
    this.updateEditCache();
    this.listOfData = this.tableData;
    if (this.tableData.length > 0) {
      this.isloading = false;
    }
    this.listOfDisplayData = [...this.listOfData];
    this.headerData = this.header;
    this.updateEditCache();
  }
}
