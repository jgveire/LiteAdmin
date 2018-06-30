<template>
    <div class="details">
        <h1>{{getFriendlyName(tableName)}} details</h1>
        <div v-for="(column, index) in tableSchema.columns" class="form__group">
            <label :for="column.name" class="form__label">{{getFriendlyName(column.name)}}</label>
            <input :name="column.name"
                   :type="getInputType(column.dataType)"
                   :value="item[column.name]"
                   :maxlength="getMaxLength(column.maxLength)"
                   class="form__control"/>
        </div>

        <div class="button__group">
            <button type="button" v-on:click="save" class="button button__primary">Save</button>
            <button type="button" v-on:click="cancel" class="button button--grey">Cancel</button>
        </div>
    </div>
</template>

<script lang="ts" src="./Details.ts"></script>
