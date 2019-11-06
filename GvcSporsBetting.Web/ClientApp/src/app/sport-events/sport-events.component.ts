import { Component, OnInit, Inject } from '@angular/core';
import { SportEvent } from '../shared/SportEvent.model';
import { SportEventsService } from '../shared/sport-events.service';
import { FormBuilder, FormArray, Validators, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-sport-events',
  templateUrl: './sport-events.component.html',
  styleUrls: ['./sport-events.component.css']
})
export class SportEventsComponent implements OnInit {
    sportEventsForms: FormArray = this.fb.array([]);
    sportEvents: SportEvent[];

    previewMode: boolean = true;
    editMode: boolean = false;
    switchButtontext: string = "Edit Mode"

    OddType = OddType;

    notification = null;

    constructor(private sportEventsService: SportEventsService,
                private fb: FormBuilder) { }

    ngOnInit() {
        this.GetAllSportEvents();
    }

    // switched between view mode & edit mode
    // reloads sports events from server
    switchMode() {
        if (this.previewMode) {
            this.editMode = true;
            this.previewMode = false;
            this.switchButtontext = "Preview Mode";
            this.GetAllSportEvents();
        } else {
            this.editMode = false;
            this.previewMode = true;
            this.switchButtontext = "Edit Mode";
            this.GetAllSportEvents();
        }
    }

    // creating a new default sport event - emty data except start date
    // add newly ceated sport event to formGroup
    addDefaultSportEvents() {
        this.sportEventsService.CreateDefaultSportEvent().subscribe(res => {
            let se = (res as any)
            this.sportEventsForms.push(this.fb.group({
                sportEventId: [se.sportEventId, Validators.required],
                eventName: [se.eventName, Validators.required],
                oddsForFirstTeam: [se.oddsForFirstTeam, Validators.min(1)],
                oddsForDraw: [se.oddsForDraw, Validators.min(1)],
                oddsForSecondTeam: [se.oddsForSecondTeam, Validators.min(1)],
                eventStartDate: [se.eventStartDate, Validators.required]
            }));

            this.showNotification(ActionCategory.Insert);
        }, error => {
              this.showNotification(ActionCategory.Error);
              console.log(error)
        });
    }

    // delete sport event and remove it from formGrpoup
    deleteSportEvent(id, i ) {
        if (confirm('Are you sure to delete this event?')) {
            this.sportEventsService.DeleteSportEvent(id).subscribe(res => {
                this.sportEventsForms.removeAt(i);
                this.showNotification(ActionCategory.Delete);
            }, error => {
                this.showNotification(ActionCategory.Error);
                console.log(error)
            });
        }
    }

    // upadtes the corresponding sportEvent
    updateSportEvent(id, fg:FormGroup) {
        this.sportEventsService.UpdateSportEvent(id, fg.value).subscribe(res => {
            this.showNotification(ActionCategory.Update);
        }, error => {
            this.showNotification(ActionCategory.Error);
            console.log(error)
        });
    }

    // gets all sport events from sever
    // ads them to spoerEventList for view mode and to formGroup for edit mode
    GetAllSportEvents() {
        this.sportEventsService.GetAllForPreview().subscribe(res => {
            this.sportEvents = <SportEvent[]>res;
            this.sportEventsForms = this.fb.array([]);
            (res as any[]).forEach((se: any) => {
                this.sportEventsForms.push(this.fb.group({
                    sportEventId: [se.sportEventId, Validators.required],
                    eventName: [se.eventName, Validators.required],
                    oddsForFirstTeam: [se.oddsForFirstTeam, Validators.min(1)],
                    oddsForDraw: [se.oddsForDraw, Validators.min(1)],
                    oddsForSecondTeam: [se.oddsForSecondTeam, Validators.min(1)],
                    eventStartDate: [se.eventStartDate, Validators.required]
                }));
            });
            
        }, error => console.log(error));
    }

    // check if spoort event's date is in the past
    eventHasPassed(date: Date): boolean {
        return new Date(date) < new Date();
    }

    // used for displaying proper messages to user for different action types
    showNotification(category: ActionCategory) {
        switch (category) {
            case ActionCategory.Insert:
                this.notification = { class: 'text-success', message: 'New Event added!' };
                break;
            case ActionCategory.Update:
                this.notification = { class: 'text-primary', message: 'Event Updated!' };
                break;
            case ActionCategory.Delete:
                this.notification = { class: 'text-danger', message: 'Event Deleted!' };
                break;
            case ActionCategory.Error:
                this.notification = { class: 'text-danger', message: 'ERRROR!!!' };
            default:
                break;
        }

        // reset notification in order for next operation to set it properly
        setTimeout(() => {
            this.notification = null;
        }, 2000);
    }

    // When an odd from an event is clicked in ‘Preview Mode’ it prints in the console  ‘A B C’
    // where A is the EventID of the event,
    // B is the type of the ODD(one of the 3 types - OddsForFirstTeam / OddsForDraw / OddsForSecondTeam)
    // and C is the Odd Value(3.25 for example)
    showOdd(id: number, odd: OddType) {
        let event: any = (this.sportEvents as any[]).find(x => x.sportEventId == id);

        let oddValue: any;
        switch (odd) {
            case OddType.FirstTeam:
                oddValue = event.oddsForFirstTeam || 'N/A';
                break;
            case OddType.Drow:
                oddValue = event.oddsForDraw || 'N/A';
                break;
            case OddType.SecondTeam:
                oddValue = event.oddsForSecondTeam || 'N/A';
            default:
                break;
        }

        console.log(event.sportEventId + ' ' + OddType[odd] + ' ' + oddValue)
    }
}

enum ActionCategory { Insert, Update, Delete, Error }

enum OddType { FirstTeam, Drow, SecondTeam }


