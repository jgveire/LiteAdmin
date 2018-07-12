<template>
    <div class="edit">
        <form novalidate class="md-layout" @submit.prevent="validateUser">
            <md-card class="md-layout-item md-small-size-100 md-medium-size-100 md-large-size-66 md-xlarge-size-50">
                <md-card-header>
                    <div class="md-title">Add {{getFriendlyName(tableName)}}</div>
                </md-card-header>

                <md-card-content>
                    <div v-for="(column, index) in tableSchema.columns"
                         :key="column.name">
                        <md-checkbox v-if="column.dataType == 'Byte' || column.dataType == 'Boolean'"
                                     :id="column.name"
                                     :name="column.name"
                                     v-model="item[column.name]"
                                     :required="!column.isNullable">{{getFriendlyName(column.name)}}</md-checkbox>
                        <md-datepicker v-else-if="column.dataType == 'DateTime'"
                                       :id="column.name"
                                       :name="column.name"
                                       v-model="item[column.name]"
                                       :maxlength="getMaxLength(column.maxLength)"
                                       :md-open-on-focus="false"
                                       :required="!column.isNullable"
                                       :class="column.isNullable ? '' : 'md-required'">
                            <label :for="column.name">{{getFriendlyName(column.name)}}</label>
                        </md-datepicker>
                        <md-field v-else-if="column.foreignKey">
                            <label :for="column.name">{{getFriendlyName(column.name)}}</label>
                            <md-select id="column.name"
                                       :name="column.name"
                                       v-model="item[column.name]"
                                       :required="!column.isNullable">
                                <md-option value="">- empty -</md-option>
                                <md-option v-for="lookup in lookups[column.foreignTable]"
                                           :key="lookup.id"
                                           :value="lookup.id">{{lookup.name}}</md-option>
                            </md-select>
                        </md-field>
                        <md-field v-else>
                            <label :for="column.name">{{getFriendlyName(column.name)}}</label>
                            <md-input :id="column.name"
                                      :name="column.name"
                                      v-model="item[column.name]"
                                      :maxlength="getMaxLength(column.maxLength)"
                                      :required="!column.isNullable"></md-input>
                            <md-button v-if="column.dataType == 'Guid' && column.isPrimaryKey"
                                       class="md-icon-button md-dense"
                                       v-on:click="generateGuid(column.name)">
                                <md-icon>cached</md-icon>
                            </md-button>
                        </md-field>
                    </div>
                </md-card-content>

                <md-progress-bar md-mode="indeterminate" v-if="sending" />

                <md-card-actions>
                    <md-button type="button" v-on:click="cancel" class="">Cancel</md-button>
                    <md-button type="button" v-on:click="save" class="md-primary">Save</md-button>
                </md-card-actions>
            </md-card>

            <md-snackbar>The item was saved with success!</md-snackbar>
        </form>

    </div>
</template>

<script lang="ts" src="./Add.ts"></script>
