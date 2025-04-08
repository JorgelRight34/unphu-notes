import { Component } from '@angular/core';
import { NavbarComponent } from '../../components/common/navbar/navbar.component';
import { SubjectListComponent } from '../../components/subject-list/subject-list.component';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-index',
  imports: [NavbarComponent, SubjectListComponent, RouterOutlet],
  templateUrl: './index.component.html',
  styleUrl: './index.component.css'
})
export class IndexComponent {

}
